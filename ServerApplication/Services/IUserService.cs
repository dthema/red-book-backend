using Domain.Models;

namespace ServerApplication.Services;

public interface IUserService : ICrudService<User>
{
    Task<User> GetByLogin(string login);
}