using Models;
using Repository.EFInitial;
using Repository.Repos.Interfaces;
using Task = Models.Task;

namespace Repository.Repos
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(DataContext dataContext) : base(dataContext) { }
    }
}
