using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class UserService : IUserService
{
    private readonly ApplicationContext _appCtx;

    public UserService(ApplicationContext appCtx)
    {
        _appCtx = appCtx;
    }

    public async Task<User> Add(User entity)
    {
        var user = _appCtx.Users.Add(entity).Entity;
        await _appCtx.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update(User entity)
    {
        var user = _appCtx.Users.Update(entity).Entity;
        await _appCtx.SaveChangesAsync();
        return user;
    }

    public async Task<bool> Delete(User entity)
    {
        _appCtx.Users.Remove(entity);
        await _appCtx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var user = new User { Id = id };
        _appCtx.Users.Attach(user);
        _appCtx.Users.Remove(user);
        await _appCtx.SaveChangesAsync();
        return true;
    }

    public async Task<User> Get(Guid id)
    {
        return await Find(id) ?? throw new InvalidOperationException();
    }

    public async Task<User?> Find(Guid id)
    {
        return await _appCtx.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));
    }

    public async Task<ICollection<User>> GetAll()
    {
        return await _appCtx.Users.ToListAsync();
    }
}