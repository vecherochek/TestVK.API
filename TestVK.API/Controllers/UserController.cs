using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestVK.API.BLL.Services;
using TestVK.API.Requests;
using TestVK.API.Responses;

namespace TestVK.API.Controllers;

[Authorize]
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
    public async Task<GetUserResponse> GetAsync(Guid userId)
    {
        var user = await _userService.GetUserAsync(userId);
        return new GetUserResponse(user);
    }
    
    [HttpGet("all-players-list")]
    public async Task<GetAllUsersResponse> GetAsync()
    {
        return new GetAllUsersResponse(
            await _userService.GetAllUsersAsync());
    }
    
    [HttpDelete]
    public async Task DeleteUserAsync(DeleteUserRequest request)
    {
        await _userService.DeleteUserAsync(request.userId);
    }
    
    [HttpPost("add-new-user")]
    public async Task AddUserAsync(AddUserRequest request)
    {
        await _userService.CreateNewUserAsync(request.login, Encoding.ASCII.GetBytes(request.password), request.userGroupCode);
    }
}