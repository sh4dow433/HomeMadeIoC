using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList;

public class Application
{
    private readonly TaskService _taskService;

    public bool IsClosed { get; set; } = false;
    public Application(TaskService taskService)
    {
        _taskService = taskService;
    }
    public async Task RunAsync()
    {
        Console.Clear();
        Console.WriteLine("MENU:");
        Console.WriteLine("1 - Add new task");
        Console.WriteLine("2 - Get list of tasks");
        Console.WriteLine("3 - Delete task");
        Console.WriteLine("4 - Mark task as finished/not finished");
        Console.WriteLine("Press any other key to leave the app.");

        var input = Console.ReadLine();
        if (int.TryParse(input, out var index))
        {
            switch (index)
            {
                case 1:
                    await _taskService.CreateNewTask();
                    break;
                case 2:
                    await _taskService.Print();
                    break;
                case 3:
                    await _taskService.DeleteTask();
                    break;
                case 4:
                    await _taskService.MarkTaskAsDone();
                    break;
                default:
                    IsClosed = true;
                    break;
            }
        }
        else
        {
            IsClosed = true;
        }
    }
}