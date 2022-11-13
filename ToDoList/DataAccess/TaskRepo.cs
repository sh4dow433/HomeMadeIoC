using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DataAccess;

public class TaskRepo
{
    private readonly AppDbContext _dbContext;

    public TaskRepo(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IEnumerable<ToDoTask>> GetTasks()
	{
		return await _dbContext.Tasks.ToListAsync();
	}

    public async Task<ToDoTask?> GetTaskById(int id)
    {
		return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

	public async Task Delete(ToDoTask task)
	{
		_dbContext.Tasks.Remove(task);
		await _dbContext.SaveChangesAsync();
	}

	public async Task Update(ToDoTask task)
	{
		_dbContext.Tasks.Update(task);
		await _dbContext.SaveChangesAsync();
	}

	public async Task Create(ToDoTask task)
	{
		_dbContext.Tasks.Add(task);
		await _dbContext.SaveChangesAsync();
	}
}
