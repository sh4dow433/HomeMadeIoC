using HomeMadeIoC.Exceptions;

namespace HomeMadeIoC.Container;

internal class DependecyGraph
{
    private readonly List<Node> _nodes = new();

    private static DependecyGraph? _dependencyGraphSingleton;
    public static DependecyGraph GetDependecyGraph => _dependencyGraphSingleton ??= new();

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
            return new UnresolvedDependencyException();
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
        // check if the node needs dependencies
        if (node.TypeDependencies.Any() == false)
        {
            return;
        }

        // clear the old dependecies
        node.Dependencies.Clear();

        // check for circular dependencies
        if (setOfNodes.Contains(node))
        {
            throw new CircularDependencyException();
        }
        setOfNodes.Add(node);

        // add the new dependencies recursively
        foreach (var type in node.TypeDependencies)
        {
            var dependency = _nodes.FirstOrDefault(n => n.ImplementationType == type);
            if (dependency == null)
            {
                throw new UnresolvedDependencyException();
            }
            if (dependency.LifeTime == LifeTime.Scoped && isSingleton)
            {
                throw new UnresolvedDependencyException(); /// CHANGE THIS EXCEPTION
            }
            SolveDependencies(dependency, setOfNodes, isSingleton);
            node.Dependencies.Add(dependency);
        }
    }
}
