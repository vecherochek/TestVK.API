using TestVK.API.BLL.Models;
using TestVK.API.BLL.Services.Interfaces;
using TestVK.API.DAL.Repositories;

namespace TestVK.API.BLL.Services;

public class UserService: IUserService
{
    private readonly UserRepository _userRepository;
    private readonly UserGroupRepository _userGroupRepository;
    private readonly UserStateRepository _userStateRepository;

    public UserService(UserRepository userRepository, UserGroupRepository userGroupRepository, UserStateRepository userStatepRepository)
    {
        _userRepository = userRepository;
        _userGroupRepository = userGroupRepository;
        _userStateRepository = userStatepRepository;
    }

    public User GetUser(Guid id)
    {
        var user = _userRepository.Get(id);
        if (user is null)
            throw new InvalidOperationException("User with this id does not exist");
        
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetUsers();
    }

    public void DeleteUser(Guid id)
    {
        var user = GetUser(id);
        var state = _userStateRepository.GetUserStateByCode("Blocked");
        
        if (state is not null)
        {
            user.UserStateId = state.Id;
        }
        
        _userRepository.Update(user);
        _userRepository.Save();
    }

    public void CreateNewUser(string login, byte[] password, string userGroupCode)
    {
        if (_userRepository.GetUserByLogin(login) is not null)
            throw new InvalidOperationException("User with this login already exists");
        
        var state = _userStateRepository.GetUserStateByCode("Active");
        var group = _userGroupRepository.GetUserGroupByCode(userGroupCode);
        
        if (group is null)
            throw new InvalidOperationException("Such group does not exist");
        if (state is null)
            throw new InvalidOperationException("Such state does not exist");
        
        //TODO: добавить кейс, когда не сущетсвует активного админа, то можно добавить нового 
        if (userGroupCode == "Admin")
        {
            var admin = _userRepository.GetUserByGroup(group.Id);
            
            if (admin is not null)
                throw new InvalidOperationException("Admin already exists. More than one admin is not allowed");
        }
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = login,
            Password = password,
            CreatedDate = DateTime.UtcNow,
            UserGroupId = group.Id,
            UserStateId = state.Id
        };
        
        _userRepository.Create(user);
        _userRepository.Save();
    }
}