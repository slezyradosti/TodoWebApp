using Models;
using Task = Models.Task;

namespace Repository.Repos.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        public Task<List<Task>> GetTaskSortedList();
    }
}
