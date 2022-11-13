using HomeMadeIoC.Api;
using Microsoft.EntityFrameworkCore;
using ToDoList;
using ToDoList.DataAccess;

IContainer container = new Container();

// register components to the IoC container
//container.AddScoped<AppDbContext>();
//container.AddScoped<TaskRepo>();
//container.AddScoped<TaskService>();
//container.AddScoped<Application>();
container.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/services.json");

// run db migrations to make sure the database is up to date
var dbContext = container.GetService<AppDbContext>();
dbContext.Database.Migrate();

// get the app from the IoC container and run it
var app = container.GetService<Application>();
while (app.IsClosed == false)
{
   await app.RunAsync();
}