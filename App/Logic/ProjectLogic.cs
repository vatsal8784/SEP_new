using App.DAOInterface;
using App.LogicInterface;
using Shared.DTO;
using Shared.Model;

namespace App.Logic;

public class ProjectLogic : IProjectLogic
{
    private readonly IProjectDAO _projectDao;
    private readonly IUserDAO _userDao;

    public ProjectLogic(IProjectDAO projectDao, IUserDAO userDao)
    {
        this._projectDao = projectDao;
        this._userDao = userDao;
    }

    public async Task<Projects> CreateProjectAsync(CreateProjectDTO dto)
    {
        User? user = await _userDao.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.UserId} was not found.");
        }

        ValidateTodo(dto);
        Projects projects= new Projects(user, dto.ProjectName);
        Projects created = await _projectDao.CreateProjectAsync(projects);
        return created;
    }

    public Task<IEnumerable<Projects>> GetProjectAsync(SearchProjectDTO searchProjectDto)
    {
        return _projectDao.GetProjectAsync(searchProjectDto);
    }

    public async Task UpdateProjectAsync(UpdateProjectDTO updateProjectDto)
    {
        Projects? existing = await _projectDao.GetByIdAsync(updateProjectDto.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {updateProjectDto.Id} not found!");
        }

        User? user = null;
        if (updateProjectDto.OwnerId != null)
        {
            user = await _userDao.GetByIdAsync((int)updateProjectDto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {updateProjectDto.OwnerId} was not found.");
            }
        }

        if (updateProjectDto.IsCompleted != null && existing.isCompleted && !(bool)updateProjectDto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = updateProjectDto.Title ?? existing.ProjectName;
        bool completedToUse = updateProjectDto.IsCompleted ?? existing.isCompleted;
    
        Projects updated = new (userToUse, titleToUse)
        {
            isCompleted = completedToUse,
            id = existing.id,
        };

        ValidateProject(updated);

        await _projectDao.UpdateProjectAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Projects? project = await _projectDao.GetByIdAsync(id);
        if (project == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        if (!project.isCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await _projectDao.DeleteAsync(id);
    }

    private void ValidateTodo(CreateProjectDTO dto)
    {
        if (string.IsNullOrEmpty(dto.ProjectName)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
    
    private void ValidateProject(Projects dto)
    {
        if (string.IsNullOrEmpty(dto.ProjectName)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}