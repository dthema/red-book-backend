using Domain.Models;

namespace ServerApplication.Services;

public interface IPlaceService : ICrudService<Place>
{
    Task<List<Place>> GetAllByCategory(Guid categoryId);
}