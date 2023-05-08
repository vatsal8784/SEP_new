using Shared.DTO;
using Shared.Model;

namespace App.LogicInterface;

public interface IUserLogic
{
    Task<User> CreateUserAsync(CreateUserDTO createUserDto);
    
    public Task<IEnumerable<User>> GetUserAsync(SearchUserDTO searchUserDto);
}