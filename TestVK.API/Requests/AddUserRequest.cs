namespace TestVK.API.Requests;

public record AddUserRequest(string login, string password, string userGroupCode);