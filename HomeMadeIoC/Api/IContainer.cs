using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Api;

public interface IContainer
{
    T GetService<T>();

    void AddSingleton<T>();
    void AddSingleton<TAbstraction, TImplementation>();

    void AddScoped<T>();
    void AddScoped<TAbstraction, TImplementation>();

    void AddServicesFromConfigurationFile(string filePath);
}
