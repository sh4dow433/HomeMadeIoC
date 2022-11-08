﻿using HomeMadeIoC.Api;
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
            ia.Hello();
          
            Console.WriteLine("hissss");
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
    static int i = 0;
    public void Hello()
    {
        Console.WriteLine("Hello fam");
    }
    public A(B b)
    {
        Console.WriteLine($"heee {i++}");
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