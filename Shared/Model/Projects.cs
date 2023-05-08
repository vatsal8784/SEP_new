namespace Shared.Model;

public class Projects
{
    public int id { get; set; }
    
    public String ProjectName { get; set; }
    
    public User Owner { get; }
    
    public ICollection<Tasks> TasksList { get; set; }
}