using Shared.DTO;
using Shared.Model;

namespace App.DAOInterface;

public interface IUserDAO
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    
    public Task<IEnumerable<User>> GetUserAsync(SearchUserDTO searchUserDto);
    
    Task<User?> GetByIdAsync(int id);
}