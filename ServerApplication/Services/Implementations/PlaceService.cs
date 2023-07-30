using Domain;
using Domain.Models;

namespace ServerApplication.Services.Implementations;

public class PlaceService : CrudService<Place>, IPlaceService
{
    public PlaceService(ApplicationContext appCtx)
        : base(appCtx) { }
}