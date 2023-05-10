namespace TestVK.API.BLL.Models;

public class User
{ 
    public Guid Id { get; set; }
    public string Login { get; set; }
    public byte[] Password { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid UserGroupId { get; set; }
    public Guid UserStateId { get; set; }
}