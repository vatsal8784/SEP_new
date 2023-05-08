using Shared.DTO;
using Shared.Model;

namespace App.DAOInterface;

public interface IProjectDAO
{
    Task<Projects> CreateProjectAsync(Projects projects);
    Task<IEnumerable<Projects>> GetProjectAsync(SearchProjectDTO searchProjectDto);
    
    Task UpdateProjectAsync(Projects updateProjectDto);
    
    Task<Projects> GetByIdAsync(int id);
    
    Task DeleteAsync(int id);
}
