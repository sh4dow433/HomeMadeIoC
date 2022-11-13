using HomeMadeIoC.Api;

namespace Test2;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            IContainer container = new Container();
            //container.AddScoped<IA, A<int, string>>();
            //container.AddScoped<B>();
            //container.AddScoped<IC, C>();
            //container.AddSingleton<D>();
            //Console.WriteLine(typeof(A<int,string>));
            container.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/json1.json");
            IA ia = container.GetService<IA>();
            A<int,string> ia2 = container.GetService<A<int, string>>();
            ia.Hello("!!!");
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
public class A<T, G> : IA
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
    public B(IC c, D d)
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