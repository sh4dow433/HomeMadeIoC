using HomeMadeIoC.Api;

var builder = WebApplication.CreateBuilder(args);
var homemadeIoC = new Container();
homemadeIoC.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/services.json");

var app = builder.Build();

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", () =>
{
    return "sall";
})
.WithName("GetWeatherForecast");

app.Run();

