namespace HomeMadeIoC.Container;

internal class Node
{
    public Type AbstractionType { get; set; }
    public Type ImplementationType { get; set; }
    public LifeTime LifeTime { get; set; }
    public List<Type> TypeDependencies { get; set; }

    public List<Node> Dependencies { get; set; } = new();
    public object? Instance { get; set; }

    public Node(Type type, LifeTime lifeTime = LifeTime.Scoped)
    {
        var ctor = type.GetConstructors()[0];
        TypeDependencies = ctor.GetParameters().Select(paramInfo => paramInfo.ParameterType).Where(p => p.IsValueType == false).ToList();
        ImplementationType = type;
        AbstractionType = type;
        LifeTime = lifeTime;
    }
    public Node(Type abstractionType, Type implementationType, LifeTime lifeTime = LifeTime.Scoped)
    {
        var ctor = implementationType.GetConstructors()[0];
        TypeDependencies = ctor.GetParameters().Select(paramInfo => paramInfo.ParameterType).Where(p => p.IsValueType == false).ToList();
        ImplementationType = implementationType;
        AbstractionType = abstractionType;
        LifeTime = lifeTime;
    }
}
