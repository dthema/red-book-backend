using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public abstract class ACrudService<T> where T : class, IEntity
{
    protected readonly ApplicationContext _appCtx;
    
    public ACrudService(ApplicationContext appCtx)
    {
        _appCtx = appCtx;
    }

    public async Task<T> Add(T entity)
    {
        var newEntity = await _appCtx.AddAsync(entity);
        await _appCtx.SaveChangesAsync();
        return newEntity.Entity;
    }

    public async Task<T> Update(T entity)
    {
        var updatedEntity = _appCtx.Update(entity);
        await _appCtx.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task<T> Delete(T entity)
    {
        var removedEntity = _appCtx.Remove(entity);
        await _appCtx.SaveChangesAsync();
        return removedEntity.Entity;
    }

    public async Task<T> DeleteById(Guid id)
    {
        var removedEntity = await _appCtx.FindAsync<T>(id) ?? throw new ArgumentException();
        _appCtx.Remove(removedEntity);
        await _appCtx.SaveChangesAsync();
        return removedEntity;
    }

    public async Task<T> Get(Guid id)
    {
        return await Find(id) ?? throw new ArgumentException();
    }

    public Task<T?> Find(Guid id)
    {
        return _appCtx.FindAsync<T>(id).AsTask();
    }

    public Task<List<T>> GetAll()
    {
        return _appCtx.Set<T>().ToListAsync();
    }
}