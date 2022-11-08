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
            //container.AddSingleton<IA, A>();
            //container.AddSingleton<B>();
            //container.AddSingleton<IC, C>();
            //container.AddSingleton<D>();
            container.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/json1.json");
            IA ia = container.GetService<IA>();
            IA ia2 = container.GetService<A>();
            ia.Hello();
            ia2.Hello();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

public interface IA
{
    void Hello();
}
public class A : IA
{
    public void Hello()
    {
        Console.WriteLine("Hello fam");
    }
    public A(B b)
    {
    }
}

public class B
{
    public B(C c, D d)
    {

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