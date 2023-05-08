namespace Shared.DTO;

public class CreateProjectDTO
{
    public int UserId { get; }
    public string ProjectName { get; }

    public CreateProjectDTO(int userId, string projectName)
    {
        UserId = userId;
        ProjectName = projectName;
    }
}