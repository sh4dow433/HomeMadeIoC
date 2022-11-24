// See https://aka.ms/new-console-template for more information
using HomeMadeIoC.Api;


IContainer container = new Container();
container.AddSingleton<IC1, C>();
container.AddScoped<IC2, C>();
container.AddScoped<B>();
container.AddScoped<A>();
var a = container.GetService<A>();

public interface IC2
{ }
public interface IC1
{ }

public class C : IC2, IC1
{
	public C()
	{
		Console.WriteLine("C");
	}
}

public class B
{
	public B(IC2 c)
	{

	}
}

public class A
{
	public A(B b, IC1 c)
	{
		Console.WriteLine("A");	
	}
}