using HomeMadeIoC.ContainerPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Api;

public class ContainerPlus : IContainer
{
    private readonly DependecyGraph _dependecyGraph = DependecyGraph.GetDependecyGraph;

    public void AddConfiguration(string path)
    {
        throw new NotImplementedException();
    }

    public void AddScoped<T>()
    {
        var service = new Service(typeof(T), LifeTime.Scoped);
        _dependecyGraph.AddService(service);
    }

    public void AddScoped<TAbstraction, TImplementation>()
    {
        var innerService = new Service(typeof(TImplementation), LifeTime.Scoped);
        var outterService = new ServiceWithAbstractionDecorator(typeof(TAbstraction), innerService);
        _dependecyGraph.AddService(outterService);
    }

    public void AddSingleton<T>()
    {
        var service = new Service(typeof(T), LifeTime.Singleton);
        _dependecyGraph.AddService(service);
    }

    public void AddSingleton<TAbstraction, TImplementation>()
    {
        var innerService = new Service(typeof(TImplementation), LifeTime.Singleton);
        var outterService = new ServiceWithAbstractionDecorator(typeof(TAbstraction), innerService);
        _dependecyGraph.AddService(outterService);
    }

    public T GetService<T>()
    {
        return (T)_dependecyGraph.GetInstance(typeof(T));
    }
}
