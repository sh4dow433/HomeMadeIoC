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
