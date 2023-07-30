using Domain;
using Domain.Models;

namespace ServerApplication.Services.Implementations;

public class UserService : CrudService<User>, IUserService
{
    public UserService(ApplicationContext appCtx)
        : base(appCtx) { }
}