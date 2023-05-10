using TestVK.API.BLL.Models;

namespace TestVK.API.BLL.Helpers;

public struct MyPage
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalUsers { get; set; }
    public List<User> Data { get; set; }
}