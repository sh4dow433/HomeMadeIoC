using HomeMadeIoC.Exceptions;

namespace HomeMadeIoC.Container;

internal class DependecyGraph
{
    public static DependecyGraph DependencyGraphInstance => _dependencyGraphSingleton ??= new();
    
    private static DependecyGraph? _dependencyGraphSingleton;
    private readonly Dictionary<Type, Node> _nodes = new();

    private DependecyGraph()
    {
        // singleton
    }

    public void AddNode(Node node)
    {
        _nodes[node.ImplementationType] = node;
        
        if (node.ImplementationType.ToString() != node.AbstractionType.ToString())
        {
            _nodes[node.AbstractionType] = node;
        }
    }

    public object GetInstance(Type type)
    {
        Node? node = _nodes[type];
        // check if the type was registered in the dependency graph 
        if (node == null)
        {
            throw new UnresolvedDependencyException(type.Name);
        }

        // check if there is an instance already created (only applies for singletons)
        if (node.Instance != null)
        {
            return node.Instance;   
        }

        // update the type to make sure we are using the implementation type, not the abstraction type
        type = node.ImplementationType;

        // result
        object? instance;

        // type's lifetime
        bool isSingleton = node.LifeTime == LifeTime.Singleton;
        
        // if the type has no dependencies try to create an instance of that type
        if (node.TypeDependencies == null || node.TypeDependencies.Any() == false)
        {
            instance = Activator.CreateInstance(type);
            if (instance == null)
            {
                throw new Exception("Object couldn't be instantiated.");
            }

            // if the registered type needs to be a singleton store the instance into a variable so it can be reused 
            if (isSingleton)
            {
                node.Instance = instance;
            }
            return instance;
        }

        // solve type's dependencies
        ResolveDependencies(node, new(), isSingleton);
        object[] builtDependencies = new object[node.Dependencies.Count];
        
        // get an instance of every dependency
        for(int i = 0; i < node.Dependencies.Count; i++)   
        {
            object? builtDependecy = GetInstance(node.Dependencies[i].ImplementationType);
            if (builtDependecy == null)
            {
                throw new Exception("Object couldn't be instantiated.");
            }
            builtDependencies[i] = builtDependecy;  
        }

        // try to instantiate our type using the built dependencies
        instance = Activator.CreateInstance(type, builtDependencies);
        if (instance == null)
        {
            throw new Exception("Object couldn't be instantiated.");
        }

        // if the registered type needs to be a singleton store the instance into a variable so it can be reused 
        if (isSingleton)
        {
            node.Instance = instance;
        }
        return instance;
    }

    private void ResolveDependencies(Node node, HashSet<Node> setOfNodes, bool isSingleton)
    {
        // check if the node's type depends on any other types
        if (node.TypeDependencies.Any() == false)
        {
            return;
        }

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
            var dependency = _nodes[type];
            if (dependency == null)
            {
                throw new UnresolvedDependencyException(type.Name);
            }
            if (dependency.LifeTime == LifeTime.Scoped && isSingleton)
            {
                throw new InvalidLifeTimeException();
            }
            ResolveDependencies(dependency, setOfNodes, isSingleton || dependency.LifeTime == LifeTime.Singleton);
            node.Dependencies.Add(dependency);
        }
    }
}
