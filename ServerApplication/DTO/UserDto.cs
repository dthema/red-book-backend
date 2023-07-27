namespace ServerApplication.DTO;

public record UserDto(Guid Id, string Login, string Password) : UnregisteredUserDto(Login, Password);