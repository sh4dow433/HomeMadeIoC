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
