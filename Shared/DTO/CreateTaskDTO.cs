namespace Shared.DTO;

public class CreateTaskDTO
{
    public int ProjectId { get; set; }
    public string Title { get; set; }
    
    public int OwnerId { get; set; }

    public CreateTaskDTO(int projectId, string title , int ownerId)
    {
        ProjectId = projectId;
        Title = title;
        OwnerId = ownerId;
    }
}