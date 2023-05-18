using Shared.DTO;
using Shared.Model;

namespace App.LogicInterface;

public interface ITaskLogic
{
    Task<Tasks> CreateTaskAsync(CreateTaskDTO dto);

    Task<IEnumerable<Tasks>> GetTasksAsync(SearchTaskDTO dto);
    
    Task UpdateAsync(UpdateTaskDTO dto);
    
    Task DeleteAsync(int id);
}