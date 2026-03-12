using Microsoft.EntityFrameworkCore;

public class TaskAccessor
{
    private readonly AppDbContext _context;

    public TaskAccessor(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetTasks(string userId)
    {
        return await _context.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTask(int id, string userId)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public async Task AddTask(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
}