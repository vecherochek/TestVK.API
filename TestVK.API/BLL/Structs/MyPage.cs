using TestVK.API.BLL.Models;

namespace TestVK.API.BLL.Structs;

public struct MyPage
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<User> Data { get; set; }
}