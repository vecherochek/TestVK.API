using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestVK.API.BLL.Services;
using TestVK.API.Requests;
using TestVK.API.Responses;

namespace TestVK.API.Controllers;

/*[Authorize]*/
[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{userId}")]
    public GetUserResponse Get(Guid userId)
    {
        var user = _userService.GetUser(userId);
        return new GetUserResponse(user);
    }
    
    [HttpGet("all-players-list")]
    public GetAllUsersResponse Get()
    {
        return new GetAllUsersResponse(
            _userService.GetAllUsers());
    }
    
    [HttpDelete("{userId}")]
    public void DeleteUser(DeleteUserRequest request)
    {
        _userService.DeleteUser(request.userId);
    }
    
    [HttpPost("add")]
    public void AddUser(AddUserRequest request)
    {
        _userService.CreateNewUser(request.login, request.password, request.userGroupCode);
    }
}