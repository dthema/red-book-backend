using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class PlaceService : CrudService<Place>, IPlaceService
{
    public PlaceService(ApplicationContext appCtx)
        : base(appCtx) { }

    public Task<List<Place>> GetAllByCategory(Guid categoryId)
    {
        return _appCtx.Places.Where(x => x.CategoryId.Equals(categoryId)).ToListAsync();
    }
}