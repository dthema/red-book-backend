namespace ServerApplication.Services;

public interface IServiceManager
{
    IUserService UserService { get; }
}