namespace Shared.DTO;

public class UpdateProjectDTO
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }

    public UpdateProjectDTO(int id)
    {
        Id = id;
    }
}