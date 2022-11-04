

namespace HomeMadeIoC.Container;

internal interface IService
{
    List<IService>? BuiltDependencies { get; set; }
    List<Type> Dependencies { get; set; }
    LifeTime LifeTime { get; set; }
    Type TypeRequired { get; set; }
    public bool AreDependenciesBuilt { get; }
    object GetInstantiatedService();
}
