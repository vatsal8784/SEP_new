namespace Shared.DTO;

public class UpdateTaskDTO
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }
    public int? ProjectId { get; set; }

    public UpdateTaskDTO(int id)
    {
        Id = id;
    }
}