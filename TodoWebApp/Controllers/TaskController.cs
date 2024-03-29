using Application.Core;
using Application.DTOs;
using Application.Handlers.Task;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApp.Controllers;

public class TaskController : BaseController
{
    private readonly ITaskHandler _taskHandler;

    public TaskController(ITaskHandler taskHandler)
    {
        _taskHandler = taskHandler;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("List")]
    public async Task<IActionResult> TaskList()
    {
        return HandleResult(await _taskHandler.GetTaskListAsync());
    }
    
    [HttpGet("Task/{id}")]
    public async Task<IActionResult> TaskDetails(Guid id)
    {
        return HandleResult(await _taskHandler.GetTaskAsync(id));
    }
    
    [HttpPost("Task")]
    public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
    {
        return HandleResult(await _taskHandler.CreateTaskAsync(taskDto));
    }
    
    [HttpPut("Task/{id}")]
    public async Task<IActionResult> EditTask(Guid id, [FromBody] TaskDto taskDto)
    {
        taskDto.Id = id;
        return HandleResult(await _taskHandler.EditTaskAsync(taskDto));
    }
    
    [HttpDelete("Task/{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        return HandleResult(await _taskHandler.DeleteTaskAsync(id));
    }
    
    [HttpPut("Task/Mark/{id}")]
    public async Task<IActionResult> MarkTask(Guid id, [FromBody]bool isDone)
    {
        return HandleResult(await _taskHandler.MarkTaskAsync(id, isDone));
    }
    
    [HttpGet("List/Filter")]
    public async Task<IActionResult> TaskFilteredList([FromQuery] string option)
    {
        Result<List<TaskDto>> result = new Result<List<TaskDto>>();
        if (option == "show_pending")
        {
            result = await _taskHandler.GetPendingTaskListAsync();
        }
        else if (option == "show_completed")
        {
            result = await _taskHandler.GetCompletedTaskListAsync();
        }

        return HandleResult(result);
    }
}