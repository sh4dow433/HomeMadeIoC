using HomeMadeIoC.Exceptions;

namespace HomeMadeIoC.ContainerPlus;

internal class Service : IService
{
    public Type TypeRequired { get; set; }
    public LifeTime LifeTime { get; set; }
    public List<Type> TypeDependencies { get; set; }


    public Service(Type type, LifeTime lifeTime = LifeTime.Scoped)
    {
        var ctor = type.GetConstructors()[0];
        TypeDependencies = ctor.GetParameters().Select(paramInfo => paramInfo.ParameterType).Where(p => p.IsValueType == false).ToList();
        TypeRequired = type;
        LifeTime = lifeTime;
    }
}

internal class Node
{
    public IService Service { get; }
    public List<Node> Dependencies { get; set; } = new();
    public object? Instance { get; set; }

    public Node(IService service)
    {
        Service = service;
    }
}

internal class DependecyGraph
{
    private readonly List<Node> _nodes = new();

    private static DependecyGraph? _dependencyGraphSingleton;
    public static DependecyGraph GetDependecyGraph => _dependencyGraphSingleton ??= new();

    private DependecyGraph()
    {
        // singleton
    }
    public void AddService(IService service)
    {
        _nodes.Add(new(service));
    }

    public object GetInstance(Type type)
    {
        Node? node = _nodes.FirstOrDefault(n => n.Service.TypeRequired == type);  
        if (node == null)
        {
            return new UnresolvedDependencyException();
        }
        if (node.Instance != null)
        {
            return node.Instance;   
        }
        object? instance;
        bool isSingleton = node.Service.LifeTime == LifeTime.Singleton;
        
        if (node.Service.TypeDependencies == null || node.Service.TypeDependencies.Any() == false)
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
            object? builtDependecy = GetInstance(node.Dependencies[i].Service.TypeRequired);
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
        if (node.Service.TypeDependencies.Any() == false)
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
        foreach (var type in node.Service.TypeDependencies)
        {
            var dependency = _nodes.FirstOrDefault(n => n.Service.TypeRequired == type);
            if (dependency == null)
            {
                throw new UnresolvedDependencyException();
            }
            if (dependency.Service.LifeTime == LifeTime.Scoped && isSingleton)
            {
                throw new UnresolvedDependencyException(); /// CHANGE THIS EXCEPTION
            }
            SolveDependencies(dependency, setOfNodes, isSingleton);
            node.Dependencies.Add(dependency);
        }
    }
}
