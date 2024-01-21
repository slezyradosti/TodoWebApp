using Application.Core;
using Application.DTOs;
using Application.Handlers.Task;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApp.Controllers;

[Route("Task")]
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
    //     var result = await _taskHandler.GetTaskListAsync();
    //     return Json(result.Value);
        return HandleResult(await _taskHandler.GetTaskListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> TaskDetails(Guid id)
    {
        return HandleResult(await _taskHandler.GetTaskAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
    {
        return HandleResult(await _taskHandler.CreateTaskAsync(taskDto));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditTask(Guid id, [FromBody] TaskDto taskDto)
    {
        taskDto.Id = id;
        return HandleResult(await _taskHandler.EditTaskAsync(taskDto));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        return HandleResult(await _taskHandler.DeleteTaskAsync(id));
    }
    
    [HttpPut("mark/{id}")]
    public async Task<IActionResult> MarkTask(Guid id, [FromBody]bool isDone)
    {
        return HandleResult(await _taskHandler.MarkTaskAsync(id, isDone));
    }
    
    [HttpGet("List/Filter")]
    public async Task<IActionResult> TaskFilteredList([FromQuery] string filterOption)
    {
        Result<List<TaskDto>> result = new Result<List<TaskDto>>();
        if (filterOption == "show_pending")
        {
            result = await _taskHandler.GetPendingTaskListAsync();
        }
        else if (filterOption == "show_completed")
        {
            result = await _taskHandler.GetCompletedTaskListAsync();
        }

        return HandleResult(result);
    }
}