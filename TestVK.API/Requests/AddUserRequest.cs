namespace TestVK.API.Requests;

public record AddUserRequest(string login, byte[] password, string userGroupCode);