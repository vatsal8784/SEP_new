namespace Shared.Model;

public class Tasks
{
    public int id { get; set; }
    
    public Projects BelongsToProjects { get; }
    
    public String title { get; }
    
    public bool isCompleted { get; set; }

    public Tasks(Projects belongsToProjects, string title)
    {
        belongsToProjects = belongsToProjects;
        title = title;
    }
}