using HomeMadeIoC.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Api;

public class Container : IContainer
{
    private readonly DependencyCollection _dependecies = DependencyCollection.GetDependencyCollectionInstance;

    public void AddConfiguration(string path)
    {
        throw new NotImplementedException();
    }

    public void AddScoped<T>()
    {
        var service = new Service(typeof(T), LifeTime.Scoped);
        _dependecies.AddService(service);
    }

    public void AddScoped<TAbstraction, TImplementation>()
    {
        var innerService = new Service(typeof(TImplementation), LifeTime.Scoped);
        var outterService = new ServiceWithAbstractionDecorator(typeof(TAbstraction), innerService);
        _dependecies.AddService(innerService);
        _dependecies.AddService(outterService);
    }

    public void AddSingleton<T>()
    {
        var service = new Service(typeof(T), LifeTime.Singleton);
        _dependecies.AddService(service);
    }

    public void AddSingleton<TAbstraction, TImplementation>()
    {
        var innerService = new Service(typeof(TImplementation), LifeTime.Singleton);
        var outterService = new ServiceWithAbstractionDecorator(typeof(TAbstraction), innerService);
        _dependecies.AddService(innerService);
        _dependecies.AddService(outterService);
    }

    public T GetService<T>()
    {
        return (T)_dependecies.GetInstantiatedService(typeof(T));
    }
}
