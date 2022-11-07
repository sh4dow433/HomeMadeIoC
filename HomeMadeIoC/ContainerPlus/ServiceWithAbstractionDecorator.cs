namespace HomeMadeIoC.ContainerPlus;

internal class ServiceWithAbstractionDecorator : IService
{
    private readonly Service _internalService;

    public List<Type> TypeDependencies { get => _internalService.TypeDependencies; set => _internalService.TypeDependencies = value; }
    public LifeTime LifeTime { get => _internalService.LifeTime; set => _internalService.LifeTime = value; }
    public Type TypeRequired { get; set; }

    public ServiceWithAbstractionDecorator(Type typeRequired, Service service)
    {
        TypeRequired = typeRequired;
        _internalService = service;
    }

}


