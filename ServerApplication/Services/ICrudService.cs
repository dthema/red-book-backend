using Domain.Models;

namespace ServerApplication.Services;

public interface ICrudService<T>
{
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(T entity);
    Task<bool> DeleteById(Guid id);
    Task<T> Get(Guid id);
    Task<T?> Find(Guid id);
    Task<List<T>> GetAll();
}