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
        var result = await _taskHandler.GetTaskListAsync();
        //return View(result.Value);
        return Json(result.Value);
    }
    
    // [HttpGet("{id}")]
    // public async Task<IActionResult> TaskDetails(Guid id)
    // {
    //     var result = await _taskHandler.GetTaskAsync(id);
    //     return View(result);
    // }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
    {
        return HandleResult(await _taskHandler.CreateTaskAsync(taskDto));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditTask(Guid id, TaskDto taskDto)
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
}