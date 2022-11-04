namespace HomeMadeIoC.Container;

internal class ServiceWithAbstractionDecorator : IService
{
    private readonly Service _internalService;

    public List<IService>? BuiltDependencies { get => _internalService.BuiltDependencies; set => _internalService.BuiltDependencies = value; }
    public List<Type> Dependencies { get => _internalService.Dependencies; set => _internalService.Dependencies = value; }
    public LifeTime LifeTime { get => _internalService.LifeTime; set => _internalService.LifeTime = value; }
    public Type TypeRequired { get; set; }

    public bool AreDependenciesBuilt => BuiltDependencies != null && Dependencies.Any();

    public ServiceWithAbstractionDecorator(Type typeRequired, Service service)
    {
        TypeRequired = typeRequired;
        _internalService = service;
    }

    public object GetInstantiatedService() => _internalService.GetInstantiatedService();
}


