using TestVK.API.BLL.Helpers;

namespace TestVK.API.Responses;

public record GetAllUsersResponse(List<MyPage> Pages);