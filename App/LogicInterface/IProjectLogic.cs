using Shared.DTO;
using Shared.Model;

namespace App.LogicInterface;

public interface IProjectLogic
{
    Task<Projects> CreateProjectAsync(CreateProjectDTO dto);
    
    Task<IEnumerable<Projects>> GetProjectAsync(SearchProjectDTO searchProjectDto);
    
    Task UpdateProjectAsync(UpdateProjectDTO updateProjectDto);
    
    Task DeleteAsync(int id);
}