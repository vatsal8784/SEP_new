namespace Shared.DTO;

public class SearchTaskDTO
{
    public String? Username { get; }
    public int? UserId { get; }
    public String? ProjectName { get; }
    public int? ProjectId { get; }
    public bool? CompletedStatus { get; }
    public String? TitleContains { get; }

    public SearchTaskDTO(String username, int userId, String projectName, int projectId, bool completedStatus,
        String titleContains)
    {
        Username = username;
        UserId = userId;
        ProjectName = projectName;
        ProjectId = projectId;
        CompletedStatus = completedStatus;
        TitleContains = titleContains;
    }
}