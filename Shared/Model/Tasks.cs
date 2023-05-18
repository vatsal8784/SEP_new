namespace Shared.Model;

public class Tasks
{
    public int id { get; set; }
    
    public Projects BelongsToProjects { get; }
    
    public String Title { get; }
    
    public bool isCompleted { get; set; }
    
    public User Owner{ get; }

    public Tasks(Projects belongsToProjects, string title , User owner)
    {
        BelongsToProjects = belongsToProjects;
        Title = title;
        Owner = owner;
    }
}