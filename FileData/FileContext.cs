using System.Text.Json;
using Shared.Model;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

    public ICollection<Tasks> Tasks
    {
        get
        {
            LoadData();
            return dataContainer!.Tasks;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }

    public ICollection<Projects> ProjectsCollection
    {
        get
        {
            LoadData();
            return dataContainer!.ProjectsCollection;
        }
    }
    
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
    
    private void LoadData()
    {
        if (dataContainer != null) return;
    
        if (!File.Exists(filePath))
        {
            dataContainer = new ()
            {
                Tasks = new List<Tasks>(),
                Users = new List<User>(),
                ProjectsCollection = new List<Projects>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
}