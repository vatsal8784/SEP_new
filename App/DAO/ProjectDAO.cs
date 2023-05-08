using App.DAOInterface;
using FileData;
using Shared.DTO;
using Shared.Model;

namespace App.DAO;

public class ProjectDAO : IProjectDAO
{
    private readonly FileContext context;

    public ProjectDAO(FileContext context)
    {
        this.context = context;
    }
    public Task<Projects> CreateProjectAsync(Projects projects)
    {
        int id = 1;
        if (context.ProjectsCollection.Any())
        {
            id = context.ProjectsCollection.Max(p => p.id);
            id++;
        }

        projects.id = id;
        
        context.ProjectsCollection.Add(projects);
        context.SaveChanges();

        return Task.FromResult(projects);
    }

    public Task<IEnumerable<Projects>> GetProjectAsync(SearchProjectDTO searchProjectDto)
    {
        IEnumerable<Projects> result = context.ProjectsCollection.AsEnumerable();

        if (!string.IsNullOrEmpty(searchProjectDto.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.ProjectsCollection.Where(project =>
                project.Owner.UserName.Equals(searchProjectDto.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchProjectDto.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == searchProjectDto.UserId);
        }

        if (searchProjectDto.CompletedStatus != null)
        {
            result = result.Where(t => t.isCompleted == searchProjectDto.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(searchProjectDto.TitleContains))
        {
            result = result.Where(t =>
                t.ProjectName.Contains(searchProjectDto.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateProjectAsync(Projects updateProjectDto)
    {
        Projects? existing = context.ProjectsCollection.FirstOrDefault(project => project.id == updateProjectDto.id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {updateProjectDto.id} does not exist!");
        }

        context.ProjectsCollection.Remove(existing);
        context.ProjectsCollection.Add(updateProjectDto);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }

    public Task<Projects> GetByIdAsync(int id)
    {
        Projects? existing = context.ProjectsCollection.FirstOrDefault(t => t.id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        Projects? existing = context.ProjectsCollection.FirstOrDefault(p => p.id == id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} does not exist!");
        }

        context.ProjectsCollection.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
}