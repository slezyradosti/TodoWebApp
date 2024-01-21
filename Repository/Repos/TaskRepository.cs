using Microsoft.EntityFrameworkCore;
using Models;
using Repository.EFInitial;
using Repository.Repos.Interfaces;
using Task = Models.Task;

namespace Repository.Repos
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(DataContext dataContext) : base(dataContext) { }
        
        public async Task<List<Task>> GetTaskSortedList()
            =>  await Context.Task
                .OrderBy(t => t.IsDone).ThenByDescending(t => t.IsDone ? t.UpdatedAt : t.CreatedAt)
                .ToListAsync();
    }
}
