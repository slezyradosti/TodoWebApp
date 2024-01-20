using Application.DTOs;
using Application.Handlers.Task;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApp.Controllers;

public class TaskController : Controller
{
    private readonly ITaskHandler _taskHandler;

    public TaskController(ITaskHandler taskHandler)
    {
        _taskHandler = taskHandler;
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> TaskList()
    {
        var result = await _taskHandler.GetTaskListAsync();
        return View(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> TaskDetails(Guid id)
    {
        var result = await _taskHandler.GetTaskAsync(id);
        return View(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskDto taskDto)
    {
        var result = await _taskHandler.CreateTaskAsync(taskDto);
        return View(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditTask(Guid id, TaskDto taskDto)
    {
        taskDto.Id = id;
        var result = await _taskHandler.EditTaskAsync(taskDto);
        return View(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var result = await _taskHandler.DeleteTaskAsync(id);
        return View(result);
    }
    
    [HttpPut("mark/{id}")]
    public async Task<IActionResult> MarkTask(Guid id, [FromBody]bool isDone)
    {
        var result = await _taskHandler.MarkTaskAsync(id, isDone);
        return View(result);
    }
}