using App.DAOInterface;
using FileData;
using Shared.DTO;
using Shared.Model;

namespace App.DAO;

public class UserDAO : IUserDAO
{
    private readonly FileContext context;

    public UserDAO(FileContext context)
    {
        this.context = context;
    }
    public Task<User> CreateUserAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetUserAsync(SearchUserDTO searchUserDto)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchUserDto.UsernameContains != null)
        {
            users = context.Users.Where(u => u.UserName.Contains(searchUserDto.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Id == id
        );
        return Task.FromResult(existing);
    }
}