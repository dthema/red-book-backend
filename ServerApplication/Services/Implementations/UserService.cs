using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class UserService : ACrudService<User>, IUserService
{
    public UserService(ApplicationContext appCtx)
        : base(appCtx) { }

    public async Task<User> GetByLogin(string login)
    {
        return await _appCtx.Users.FirstOrDefaultAsync(x => x.Login.Equals(login)) ?? throw new ArgumentException();
    }
}