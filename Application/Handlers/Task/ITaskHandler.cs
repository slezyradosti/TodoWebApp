using Application.Core;
using Application.DTOs;

namespace Application.Handlers.Task
{
    public interface ITaskHandler
    {
        public Task<Result<List<TaskDto>>> GetTaskListAsync();
        public Task<Result<TaskDto>> GetTaskAsync(Guid taskId);
        public Task<Result<string>> CreateTaskAsync(TaskDto taskDto);
        public Task<Result<string>> EditTaskAsync(TaskDto taskDto);
        public Task<Result<string>> DeleteTaskAsync(Guid taskId);
        public Task<Result<string>> MarkTaskAsync(Guid taskId, bool isDone);
    }
}
