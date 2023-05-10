using TestVK.API.BLL.Structs;

namespace TestVK.API.Responses;

public record GetAllUsersResponse(List<MyPage> Pages);