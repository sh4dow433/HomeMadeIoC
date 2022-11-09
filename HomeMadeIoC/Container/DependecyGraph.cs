using HomeMadeIoC.Exceptions;

namespace HomeMadeIoC.Container;

internal class DependecyGraph
{
    public static DependecyGraph DependencyGraphInstance => _dependencyGraphSingleton ??= new();
    
    private static DependecyGraph? _dependencyGraphSingleton;
    private readonly List<Node> _nodes = new();

    private DependecyGraph()
    {
        // singleton
    }

    public void AddNode(Node node)
    {
        _nodes.Add(node);   
    }

    public object GetInstance(Type type)
    {
        Node? node = _nodes.FirstOrDefault(n => n.AbstractionType == type || n.ImplementationType == type);
        if (node == null)
        {
            throw new UnresolvedDependencyException(type.Name);
        }
        if (node.Instance != null)
        {
            return node.Instance;   
        }
        type = node.ImplementationType;
        object? instance;
        bool isSingleton = node.LifeTime == LifeTime.Singleton;
        
        if (node.TypeDependencies == null || node.TypeDependencies.Any() == false)
        {
            instance = Activator.CreateInstance(type);
            if (instance == null)
            {
                throw new Exception("Object couldn't be instantiated.");
            }
            if (isSingleton)
            {
                node.Instance = instance;
            }
            return instance;
        }
        SolveDependencies(node, new(), isSingleton);
        object[] builtDependencies = new object[node.Dependencies.Count];
        
        for(int i = 0; i < node.Dependencies.Count; i++)   
        {
            object? builtDependecy = GetInstance(node.Dependencies[i].ImplementationType);
            if (builtDependecy == null)
            {
                throw new Exception("Object couldn't be instantiated.");
            }
            builtDependencies[i] = builtDependecy;  
        }
        instance = Activator.CreateInstance(type, builtDependencies);
        if (instance == null)
        {
            throw new Exception("Object couldn't be instantiated.");
        }
        if (isSingleton)
        {
            node.Instance = instance;
        }
        return instance;
    }

    private void SolveDependencies(Node node, HashSet<Node> setOfNodes, bool isSingleton)
    {
        // check if the node's type depends on any other type
        if (node.TypeDependencies.Any() == false)
        {
            return;
        }

        // clear the old dependencies
        // node.Dependencies.Clear();

        // check if the dependencies were already solved
        if (node.Dependencies.Any() == true)
        {
            return;
        }

        // check for circular dependencies
        if (node.TypeDependencies.Any(t => t == node.AbstractionType || t == node.ImplementationType))
        {
            throw new CircularDependencyException();
        }
        if (setOfNodes.Contains(node))
        {
            throw new CircularDependencyException();
        }
        setOfNodes.Add(node);

        // solve dependencies recursively
        foreach (var type in node.TypeDependencies)
        {
            var dependency = _nodes.FirstOrDefault(n => n.ImplementationType == type || n.AbstractionType == type);
            if (dependency == null)
            {
                throw new UnresolvedDependencyException(type.Name);
            }
            if (dependency.LifeTime == LifeTime.Scoped && isSingleton)
            {
                throw new InvalidLifeTimeException();
            }
            SolveDependencies(dependency, setOfNodes, isSingleton || dependency.LifeTime == LifeTime.Singleton);
            node.Dependencies.Add(dependency);
        }
    }
}
