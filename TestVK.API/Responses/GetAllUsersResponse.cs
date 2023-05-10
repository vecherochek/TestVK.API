using TestVK.API.BLL.Models;

namespace TestVK.API.Responses;

public record GetAllUsersResponse(IEnumerable<User> Users);