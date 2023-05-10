using TestVK.API.BLL.Helpers;
using TestVK.API.BLL.Models;
using TestVK.API.BLL.Services.Interfaces;
using TestVK.API.DAL.Repositories;

namespace TestVK.API.BLL.Services;

public class UserService: IUserService
{
    private readonly UserRepository _userRepository;
    private readonly UserGroupRepository _userGroupRepository;
    private readonly UserStateRepository _userStateRepository;

    public UserService(UserRepository userRepository, UserGroupRepository userGroupRepository, UserStateRepository userStateRepository)
    {
        _userRepository = userRepository;
        _userGroupRepository = userGroupRepository;
        _userStateRepository = userStateRepository;
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user is null)
            throw new InvalidOperationException("User with this id does not exist");
        
        return user;
    }
    
    public async Task<List<MyPage>> GetAllUsersAsync()
    {
        var pageSize = 10;
        
        var users = await _userRepository.GetUsersAsync();
        if (!users.Any())
        {
            var page = new MyPage
            {
                PageNumber = 1,
                PageSize = pageSize,
                TotalUsers = 0,
                TotalPages = 1,
                Data = null
            };
            return new List<MyPage>(){page};
        }
        
        var totalUsers = users.Count();
        var totalPages = totalUsers / pageSize + 1;

        var pages = users.Select((item, index) => new { Item = item, Index = index })
            .GroupBy(x => x.Index / pageSize)
            .Select(g => new MyPage
            {
                PageNumber = g.Key + 1,
                PageSize = pageSize,
                TotalUsers = totalUsers,
                TotalPages = totalPages,
                Data = g.Select(x => x.Item).ToList()
            })
            .ToList();
        
        return pages;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await GetUserAsync(id);
        var state = await _userStateRepository.GetUserStateByCodeAsync("Blocked");
        
        if (state is not null)
        {
            user.UserStateId = state.Id;
        }
        
        _userRepository.Update(user);
        await _userRepository.SaveAsync();
    }

    public async Task CreateNewUserAsync(string login, byte[] password, string userGroupCode)
    {
        if (await _userRepository.GetUserByLoginAsync(login) is not null)
            throw new InvalidOperationException("User with this login already exists");
        
        var state = await _userStateRepository.GetUserStateByCodeAsync("Active");
        var group = await _userGroupRepository.GetUserGroupByCodeAsync(userGroupCode);
        
        if (group is null)
            throw new InvalidOperationException("Such group does not exist");
        if (state is null)
            throw new InvalidOperationException("Such state does not exist");
        
        //TODO: добавить кейс, когда не сущетсвует активного админа, то можно добавить нового 
        if (userGroupCode == "Admin")
        {
            var admin = _userRepository.GetUserByGroupAsync(group.Id);
            
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
        
        await _userRepository.CreateAsync(user);
        await _userRepository.SaveAsync();
    }
}