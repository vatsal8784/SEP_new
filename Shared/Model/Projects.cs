namespace Shared.Model;

public class Projects
{
    public int id { get; set; }
    
    public String ProjectName { get; set; }
    
    public string Owner { get; set; }
    
    public bool isCompleted { get; set; }
    
    public List<User> UsersOfProject { get; set; }
    
}