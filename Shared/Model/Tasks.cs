namespace Shared.Model;

public class Tasks
{
    public int id { get; set; }
    
    public int projectId{ get; set; }
    
    public String Title { get; set; }
    
    public bool isCompleted { get; set; }
    
}