namespace Shared.Model;

public class Projects
{
    public int id { get; set; }
    
    public String ProjectName { get; set; }
    
    public User Owner { get; }
    
    public bool isCompleted { get; set; }
    
    public ICollection<Tasks> TasksList { get; set; }
    
    public Projects(User owner, string title)
    {
        Owner = owner;
        ProjectName = title;
    }
}