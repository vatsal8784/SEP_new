namespace Shared.DTO;

public class CreateUserDTO
{
    public string UserName{ get; init; }
    public string Password{ get; init; }

    public CreateUserDTO(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
    public CreateUserDTO()
    {
    }
}