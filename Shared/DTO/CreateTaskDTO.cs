namespace Shared.DTO;

public class CreateTaskDTO
{
    public int ProjectId { get; set; }
    public string Title { get; set; }

    public CreateTaskDTO(int projectId, string title)
    {
        ProjectId = projectId;
        Title = title;
    }
}