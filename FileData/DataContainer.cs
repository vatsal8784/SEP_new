using Shared.Model;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Tasks> Tasks { get; set; }
    public ICollection<Projects> ProjectsCollection { get; set; }
}