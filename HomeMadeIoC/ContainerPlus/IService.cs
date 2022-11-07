

namespace HomeMadeIoC.ContainerPlus;

internal interface IService
{
    List<Type> TypeDependencies { get; set; }
    LifeTime LifeTime { get; set; }
    Type TypeRequired { get; set; }
}
