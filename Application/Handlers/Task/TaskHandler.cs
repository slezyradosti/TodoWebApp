using Application.Core;
using Application.DTOs;
using AutoMapper;
using Repository.Repos.Interfaces;

namespace Application.Handlers.Task
{
    public class TaskHandler : ITaskHandler
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<TaskDto>>> GetTaskListAsync()
        {
            var taskList = await _taskRepository.GetTaskSortedList();

            List<TaskDto> taskDtoList = new List<TaskDto>();

            _mapper.Map(taskList, taskDtoList);

            return Result<List<TaskDto>>.Success(taskDtoList);
        }
        
        public async Task<Result<TaskDto>> GetTaskAsync(Guid taskId)
        {
            var taskList = await _taskRepository.GetOneAsync(taskId);

            TaskDto taskDto = new TaskDto();

            _mapper.Map(taskList, taskDto);
            
            return Result<TaskDto>.Success(taskDto);
        }
        
        public async Task<Result<string>> CreateTaskAsync(TaskDto taskDto)
        {
            var task = new Models.Task();
            
            _mapper.Map(taskDto, task);

            var result = await _taskRepository.AddAsync(task) > 0;

            if (!result) return Result<string>.Failure("Failed to create Task");
            return Result<string>.Success("Successfully");
        }
        
        public async Task<Result<string>> EditTaskAsync(TaskDto taskDto)
        {
            var task = await _taskRepository.GetOneAsync(taskDto.Id);
            if (task == null) return null;
            
            _mapper.Map(taskDto, task);

            var result = await _taskRepository.SaveAsync(task) > 0;

            if (!result) return Result<string>.Failure("Failed to update Task");
            return Result<string>.Success("Successfully");
        }
        
        public async Task<Result<string>> DeleteTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.GetOneAsync(taskId);
            if (task == null) return null;

            var result = await _taskRepository.RemoveAsync(task) > 0;
            
            if (!result) return Result<string>.Failure("Failed to delete Task");
            return Result<string>.Success("Successfully");
        }
        
        public async Task<Result<string>> MarkTaskAsync(Guid taskId, bool isDone)
        {
            var task = await _taskRepository.GetOneAsync(taskId);
            if (task == null) return null;

            task.IsDone = isDone;

            var result = await _taskRepository.SaveAsync(task) > 0;

            if (!result) return Result<string>.Failure("Failed to update Task");
            return Result<string>.Success("Successfully");
        }
        
        public async Task<Result<List<TaskDto>>> GetCompletedTaskListAsync()
        {
            var taskList = await _taskRepository.GetCompletedTaskSortedList();

            List<TaskDto> taskDtoList = new List<TaskDto>();

            _mapper.Map(taskList, taskDtoList);

            return Result<List<TaskDto>>.Success(taskDtoList);
        }
        
        public async Task<Result<List<TaskDto>>> GetPendingTaskListAsync()
        {
            var taskList = await _taskRepository.GetPendingTaskSortedList();

            List<TaskDto> taskDtoList = new List<TaskDto>();

            _mapper.Map(taskList, taskDtoList);

            return Result<List<TaskDto>>.Success(taskDtoList);
        }
    }
}
