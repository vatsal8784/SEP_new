using App.DAO;
using App.DAOInterface;
using App.LogicInterface;
using Shared.DTO;
using Shared.Model;

namespace App.Logic;

public class TaskLogic : ITaskLogic
{
    private readonly ITaskDAO TaskDao;
    private readonly IUserDAO UserDao;
    private readonly IProjectDAO ProjectDao;

    public TaskLogic(ITaskDAO taskDao, IProjectDAO projectDao, IUserDAO userDao)
    {
        this.TaskDao = taskDao;
        this.ProjectDao = projectDao;
        this.UserDao = userDao;
    }
    public async Task<Tasks> CreateTaskAsync(CreateTaskDTO dto)
    {
        Projects? projects = await ProjectDao.GetByIdAsync(dto.ProjectId);
        if (projects == null)
        {
            throw new Exception($"User with id {dto.ProjectId} was not found.");
        }

        User? user = await UserDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }
        
        ValidateTask(dto);
        Tasks tasks = new Tasks(projects , dto.Title , user);
        Tasks created = await TaskDao.CreateTaskAsync(tasks);
        return created;

    }

    public Task<IEnumerable<Tasks>> GetTasksAsync(SearchTaskDTO dto)
    {
        return TaskDao.GetTasksAsync(dto);
    }

    public async Task UpdateAsync(UpdateTaskDTO dto)
    {
        Tasks? existing = await TaskDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await UserDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        Projects projects = null;
        if (dto.ProjectId != null)
        {
            projects = await ProjectDao.GetByIdAsync((int)dto.ProjectId);
            if (projects == null)
            {
                throw new Exception($"Project with id {dto.ProjectId} was not found.");
            }
        }

        if (dto.IsCompleted != null && existing.isCompleted && !(bool)dto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        Projects project = projects ?? existing.BelongsToProjects;
        bool completedToUse = dto.IsCompleted ?? existing.isCompleted;
    
        Tasks updated = new (project , titleToUse , userToUse)
        {
            isCompleted = completedToUse,
            id = existing.id,
        };

        ValidateTodo(updated);

        await TaskDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Tasks? tasks = await TaskDao.GetByIdAsync(id);
        if (tasks == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        if (!tasks.isCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await TaskDao.DeleteAsync(id);
    }

    private void ValidateTask(CreateTaskDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
    
    private void ValidateTodo(Tasks dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}