using HomeMadeIoC.ConfigurationParser;
using HomeMadeIoC.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Api;

public class Container : IContainer
{
    private readonly DependecyGraph _dependecyGraph = DependecyGraph.DependencyGraphInstance;
    public void AddServicesFromConfigurationFile(string path)
    {
        var listOfServices = JsonFileParser.GetServicesFromFile(path);
        foreach(var service in listOfServices)
        {
            Type? type = Type.GetType(service.Type + ", " + service.Type.Split('.')[0]);
            LifeTime lifeTime = LifeTime.Scoped;
            if (service.IsSingleton)
            {
                lifeTime = LifeTime.Singleton;
            }
            if (type == null)
            {
                throw new Exception($"{service.Type} couldn't be found");
            }
            if (service.Abstraction != null && service.Abstraction.Any())
            {
                Type? abstraction = Type.GetType(service.Abstraction + ", " + service.Abstraction.Split('.')[0]);
                if (abstraction == null)
                {
                    throw new Exception($"{service.Abstraction} couldn't be found");
                }
                var node = new Node(abstraction, type, lifeTime);
                _dependecyGraph.AddNode(node);
            }
            else
            {
                var node = new Node(type, lifeTime);
                _dependecyGraph.AddNode(node);
            }
        }
    }

    public void AddScoped<T>()
    {
        var node = new Node(typeof(T));
        _dependecyGraph.AddNode(node);
    }

    public void AddScoped<TAbstraction, TImplementation>()
    { 
        var node = new Node(typeof(TAbstraction), typeof(TImplementation));
        _dependecyGraph.AddNode(node);
    }

    public void AddSingleton<T>()
    { 
        var node = new Node(typeof(T), LifeTime.Singleton);
        _dependecyGraph.AddNode(node);
    }

    public void AddSingleton<TAbstraction, TImplementation>()
    {
        var node = new Node(typeof(TAbstraction), typeof(TImplementation), LifeTime.Singleton);
        _dependecyGraph.AddNode(node);
    }

    public T GetService<T>()
    {
        return (T)_dependecyGraph.GetInstance(typeof(T));
    }
}
