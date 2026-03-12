using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("tasks")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly TaskManager _taskManager;

    public TaskController(TaskManager taskManager)
    {
        _taskManager = taskManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var tasks = await _taskManager.GetTasks(userId);

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if(userId == null)
            return Unauthorized();

        var task = await _taskManager.CreateTask(request.Title, userId);

        return Ok(task);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteTask(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var success = await _taskManager.CompleteTask(id, userId);

        if (!success)
            return NotFound();

        return Ok();
    }
}