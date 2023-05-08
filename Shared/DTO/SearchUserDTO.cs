namespace Shared.DTO;

public class SearchUserDTO
{
    public string? UsernameContains { get;  }

    public SearchUserDTO(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
}