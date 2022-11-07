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
        IContainer container = new ContainerPlus();
        //container.AddScoped<IA, A>();
        container.AddScoped<A>();
        container.AddScoped<B>();
        container.AddScoped<C>();
        container.AddSingleton<D>();

        // IA ia = container.GetService<IA>();
        // IA ia2 = container.GetService<IA>();
        IA ia =  container.GetService<A>();
        ia.Hello();
        Console.WriteLine("hi");
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
    public A(B b, D d)
    {

    }
}

public class B
{
    public B(C c, D d)
    {

    }
}

public class C
{
    public C()
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