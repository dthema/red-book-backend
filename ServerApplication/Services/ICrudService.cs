using Domain.Models;

namespace ServerApplication.Services;

public interface ICrudService<T> where T : IEntity
{
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
    Task<T> DeleteById(Guid id);
    Task<T> Get(Guid id);
    Task<T?> Find(Guid id);
    Task<List<T>> GetAll();
}