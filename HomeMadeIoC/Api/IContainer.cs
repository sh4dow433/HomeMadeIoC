using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Api;

public interface IContainer
{
    T GetService<T>();

    void AddSingleton<T>() where T: class, new();
    void AddSingleton<TAbstraction, TImplementation>() where TImplementation : class, new();

    void AddScoped<T>() where T : class, new();
    void AddScoped<TAbstraction, TImplementation>() where TImplementation : class, new();

    void AddConfiguration(string path);
}
