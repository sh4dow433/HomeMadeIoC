using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Api;

public interface IContainer
{
    T GetService<T>();

    void AddSingleton<T>() where T : class;
    void AddSingleton<TAbstraction, TImplementation>() where TImplementation : class;

    void AddScoped<T>() where T : class;
    void AddScoped<TAbstraction, TImplementation>() where TImplementation : class;

    void AddServicesFromConfigurationFile(string filePath);
}
