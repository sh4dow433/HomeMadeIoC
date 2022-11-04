using HomeMadeIoC.Exceptions;

namespace HomeMadeIoC.Container;

internal class Service : IService
{
    public Type TypeRequired { get; set; }
    public LifeTime LifeTime { get; set; }
    public List<Type> Dependencies { get; set; }
    public List<IService>? BuiltDependencies { get; set; }
    public bool AreDependenciesBuilt => BuiltDependencies != null && Dependencies.Any();


    private object? Singleton { get; set; }
    public Service(Type type, LifeTime lifeTime = LifeTime.Scoped)
    {
        var ctor = type.GetConstructors()[0];
        Dependencies = ctor.GetParameters().Select(paramInfo => paramInfo.ParameterType).ToList();
        TypeRequired = type;
        LifeTime = lifeTime;
    }

    public object GetInstantiatedService()
    {
        if (LifeTime == LifeTime.Singleton)
        {
            if (Singleton != null)
            {
                return Singleton;
            }
            Singleton = InstantiateObject();
            return Singleton;
        }

        return InstantiateObject();
    }


    private object InstantiateObject()
    {
        object? instance;
        if (BuiltDependencies == null || BuiltDependencies.Count == 0)
        {
            instance = Activator.CreateInstance(TypeRequired);
        }
        else
        {
            object?[] dependencies = new object[BuiltDependencies.Count];
            for (int i = 0; i < BuiltDependencies.Count; i++)
            {
                dependencies[i] = BuiltDependencies[i].GetInstantiatedService();
            }
            instance = Activator.CreateInstance(TypeRequired, dependencies);
        }
        if (instance == null)
        {
            throw new UnresolvedDependencyException();
        }
        return instance;
    }
}


