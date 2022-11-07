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

    public void AddConfiguration(string path)
    {
        throw new NotImplementedException();
    }

    public void AddScoped<T>() where T : class, new()
    {
        var node = new Node(typeof(T));
        _dependecyGraph.AddNode(node);
    }

    public void AddScoped<TAbstraction, TImplementation>() where TImplementation : class, new()
    {
        var node = new Node(typeof(TAbstraction), typeof(TImplementation));
        _dependecyGraph.AddNode(node);
    }

    public void AddSingleton<T>() where T : class, new()
    { 
        var node = new Node(typeof(T), LifeTime.Singleton);
        _dependecyGraph.AddNode(node);
    }

    public void AddSingleton<TAbstraction, TImplementation>() where TImplementation : class, new()
    {
        var node = new Node(typeof(TAbstraction), typeof(TImplementation), LifeTime.Singleton);
        _dependecyGraph.AddNode(node);
    }

    public T GetService<T>()
    {
        return (T)_dependecyGraph.GetInstance(typeof(T));
    }
}
