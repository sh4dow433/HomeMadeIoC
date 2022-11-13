using ToDoList.DataAccess;

namespace ToDoList;

public class TaskService
{
    private readonly TaskRepo _repo;
    public TaskService(TaskRepo repo)
    {
        _repo = repo;
    }
    public async Task CreateNewTask()
    {
        Console.Clear();
        Console.WriteLine("Create new task");
        Console.WriteLine("Title:");
        var input = Console.ReadLine();

        var task = new ToDoTask()
        {
            Title = input!
        };
        await _repo.Create(task);
    }
    public async Task Print()
    {
        var tasks = await _repo.GetTasks();
        Console.Clear();
        Console.WriteLine("List of tasks");
        foreach (var task in tasks)
        {
            Console.WriteLine($"Id: {task.Id} | Title: {task.Title} | Is finished: {task.IsDone}\n");
        }
        Console.WriteLine("\nPress any key to go back to the menu");
        Console.ReadKey();
    }

    public async Task MarkTaskAsDone()
    {
        Console.Clear();
        Console.WriteLine("Which task do you want to mark as finished/not finished? Use 0 to exit");
        var input = Console.ReadLine();
        if (int.TryParse(input, out var id))
        {
            if (id == 0)
            {
                return;
            }
            var task = await _repo.GetTaskById(id);
            if (task == null)
            {
                Console.WriteLine("Task couldn't be found.\n\n");
                await MarkTaskAsDone();
                return;
            }
            task.IsDone = !task.IsDone;
            await _repo.Update(task);
        }
        else
        {
            Console.WriteLine("You didn't enter a valid ID. Try again\n\n");
            await MarkTaskAsDone();
        }
    }

    public async Task DeleteTask()
    {
        Console.Clear();
        Console.WriteLine("Which task do you want to delete? Use 0 to exit");
        var input = Console.ReadLine();
        if (int.TryParse(input, out var id))
        {
            if (id == 0)
            {
                return;
            }
            var task = await _repo.GetTaskById(id);
            if (task == null)
            {
                Console.WriteLine("Task couldn't be found.\n\n");
                await DeleteTask();
                return;
            }
            await _repo.Delete(task);
        }
        else
        {
            Console.WriteLine("You didn't enter a valid ID. Try again.\n\n");
            await DeleteTask();
        }
    }
}

