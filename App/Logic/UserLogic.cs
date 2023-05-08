using App.DAOInterface;
using App.LogicInterface;
using Shared.DTO;
using Shared.Model;

namespace App.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO UserDao;

    public UserLogic(IUserDAO userDao)
    {
        this.UserDao = userDao;
    }
    public async Task<User> CreateUserAsync(CreateUserDTO createUserDto)
    {
        User? existing = await UserDao.GetByUsernameAsync(createUserDto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(createUserDto);
        User toCreate = new User
        {
            UserName = createUserDto.UserName,
            Password = createUserDto.Password
        };
        
        User created = await UserDao.CreateUserAsync(toCreate);
        
        return created;
    }

    public Task<IEnumerable<User>> GetUserAsync(SearchUserDTO searchUserDto)
    {
        return UserDao.GetUserAsync(searchUserDto);
    }

    private static void ValidateData(CreateUserDTO userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}