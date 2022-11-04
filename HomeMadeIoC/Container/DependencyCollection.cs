using HomeMadeIoC.Exceptions;

namespace HomeMadeIoC.Container;

internal class DependencyCollection
{
    // STATIC
    private static DependencyCollection? _instance = null;
    public static DependencyCollection GetDependencyCollectionInstance => _instance ??= new();
    
    private readonly List<IService> _services = new();

    // SINGLETON
    private DependencyCollection()
    {
        //empty ctor
    }


    public bool AddService(IService service)
    {
        if (_services.Where(s => s.TypeRequired == service.TypeRequired).Any())
        {
            return false;
        }

        _services.Add(service);
        return true;
    }

    public object GetInstantiatedService(Type type)
    {
        var service = _services.FirstOrDefault(s => s.TypeRequired == type);

        if (service == null)
        {
            throw new UnresolvedDependencyException();
        }

        if (service.AreDependenciesBuilt)
        {
            return service.GetInstantiatedService();
        }

        BuildDependencies(service);
        CheckForCircularDependency(service, type);
        return service.GetInstantiatedService();
    }



    // PRIVATE METHODS:
    //private void BuildDependencies(IService service)
    //{
    //    // foreach dependency type create a dependency service and add them to the BuiltDependencies
    //    // check if there are any circular dependencies
    //    foreach (var type in service.Dependencies)
    //    {
    //        var dependency = _services.FirstOrDefault(s => s.TypeRequired == type);
    //        if (dependency == null)
    //        {
    //            throw new UnresolvedDependencyException();
    //        }
    //        //var builtDependency = dependency.GetInstantiatedService();
    //        //var builtDependecy = GetInstantiatedService(dependency.TypeRequired);
    //        //var dependencies = builtDependecy;
    //        BuildDependencies(dependency);
    //        service.BuiltDependencies?.Add(dependency);
    //        if(CheckForCircularDependency(service, service.TypeRequired))
    //        {
    //            throw new CircularDependencyException();
    //        }
    //    }
    //}
    private void BuildDependencies(IService service)
    {
        if (service.Dependencies.Any() == false)
        {
            return;
        }
        service.BuiltDependencies = new();
        foreach (var type in service.Dependencies)
        {
            var dep = _services.FirstOrDefault(service => service.TypeRequired == type);
            if (dep == null)
            {
                throw new UnresolvedDependencyException();
            }
            BuildDependencies(dep);
            service.BuiltDependencies.Add(dep);
        }
    }

    private bool CheckForCircularDependency(IService service, Type type)
    {
        if (service.BuiltDependencies == null)
        {
            return false;
        }
        foreach (var dependency in service.BuiltDependencies)
        {
            if (dependency.TypeRequired == type)
            {
                return true;
            }
            if (CheckForCircularDependency(dependency, type) == true)
            {
                return true;
            }
        }
        return false;
    }

}


