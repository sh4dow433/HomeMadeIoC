using HomeMadeIoC.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            IContainer container = new Container();
            //container.AddScoped<IA, A>();
            //container.AddScoped<B>();
            //container.AddScoped<IC, C>();
            //container.AddSingleton<D>();
            container.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/json1.json");
            IA ia = container.GetService<IA>();
            A ia2 = container.GetService<A>();
            ia.Hello("fam");
            ia2.Hello("mate");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

public interface IA
{
    void Hello(string? name);
}
public class A : IA
{
    public void Hello(string? name)
    {
        Console.WriteLine($"Hello {name}");
    }
    public A(B b)
    {
        Console.WriteLine("Hi from A");
    }
}

public class B
{
    public B(C c, D d)
    {
        Console.WriteLine("Hi from B");
    }
}

public interface IC { }
public class C : IC
{
    public C(D d)
    {
        Console.WriteLine("hi from C");
    }
}

public class D
{
    public D()
    {
        Console.WriteLine("hi from D");
    }
}