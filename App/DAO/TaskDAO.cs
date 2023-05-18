using App.DAOInterface;
using FileData;
using Shared.DTO;
using Shared.Model;

namespace App.DAO;

public class TaskDAO : ITaskDAO
{
    private readonly FileContext context;

    public TaskDAO(FileContext context)
    {
        this.context = context;
    }
    public Task<Tasks> CreateTaskAsync(Tasks tasks)
    {
        int id = 1;
        if (context.Tasks.Any())
        {
            id = context.Tasks.Max(t => t.id);
            id++;
        }

        tasks.id = id;
        
        context.Tasks.Add(tasks);
        context.SaveChanges();

        return Task.FromResult(tasks);
    }

    public Task<IEnumerable<Tasks>> GetTasksAsync(SearchTaskDTO dto)
    {
        IEnumerable<Tasks> result = context.Tasks.AsEnumerable();

        if (!string.IsNullOrEmpty(dto.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.Tasks.Where(todo =>
                todo.Owner.UserName.Equals(dto.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (dto.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == dto.UserId);
        }
        
        if (!string.IsNullOrEmpty(dto.ProjectName))
        {
            result = context.Tasks.Where(todo =>
                todo.BelongsToProjects.ProjectName.Equals(dto.ProjectName, StringComparison.OrdinalIgnoreCase));
        }

        if (dto.ProjectId != null)
        {
            result = result.Where(t => t.BelongsToProjects.id == dto.ProjectId);
        }

        if (dto.CompletedStatus != null)
        {
            result = result.Where(t => t.isCompleted == dto.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(dto.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(dto.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Tasks tasks)
    {
        Tasks? existing = context.Tasks.FirstOrDefault(todo => todo.id == tasks.id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {tasks.id} does not exist!");
        }

        context.Tasks.Remove(existing);
        context.Tasks.Add(tasks);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }

    public Task<Tasks> GetByIdAsync(int id)
    {
        Tasks? existing = context.Tasks.FirstOrDefault(t => t.id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        Tasks? existing = context.Tasks.FirstOrDefault(todo => todo.id == id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} does not exist!");
        }

        context.Tasks.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
}