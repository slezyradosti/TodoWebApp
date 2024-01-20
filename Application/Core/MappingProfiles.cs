using Application.DTOs;
using Task = Models.Task;

namespace Application.Core;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles() 
    { 
        CreateMap<Task, TaskDto>();
        CreateMap<TaskDto, Task>()
            .ForMember(x => x.CreatedAt, y => y.Ignore());
    }
}