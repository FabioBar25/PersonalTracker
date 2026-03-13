public class TaskManager
{
    private readonly TaskAccessor _taskAccessor;

    public TaskManager(TaskAccessor taskAccessor)
    {
        _taskAccessor = taskAccessor;
    }

    public async Task<List<TaskItem>> GetTasks(string userId)
    {
        return await _taskAccessor.GetTasks(userId);
    }

    public async Task<TaskItem> CreateTask(string title, string userId)
    {
        var task = new TaskItem
        {
            Title = title,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _taskAccessor.AddTask(task);

        return task;
    }

    public async Task<bool> CompleteTask(int taskId, string userId)
    {
        var task = await _taskAccessor.GetTask(taskId, userId);

        if (task == null)
            return false;

        task.IsCompleted = true;

        await _taskAccessor.UpdateTask(task);

        return true;
    }
}