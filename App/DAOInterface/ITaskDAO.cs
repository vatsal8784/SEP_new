using Shared.DTO;
using Shared.Model;

namespace App.DAOInterface;

public interface ITaskDAO
{
    Task<Tasks> CreateTaskAsync(Tasks tasks);
    
    Task<IEnumerable<Tasks>> GetTasksAsync(SearchTaskDTO dto);
    
    Task UpdateAsync(Tasks tasks);
    
    Task<Tasks> GetByIdAsync(int id);
    
    Task DeleteAsync(int id);
}