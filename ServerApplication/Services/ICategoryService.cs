using Domain.Models;

namespace ServerApplication.Services;

public interface ICategoryService : ICrudService<Category>
{
    Task<List<Category>> GetAllByUser(Guid userId);
}