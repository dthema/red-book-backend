using Domain.Models;

namespace ServerApplication.Services;

public interface IGeoService
{
    Place GetNearestPlace(Geopoint location);
    PlacesChain GetNearestChain(Geopoint location);
    Place GetNearestPlaceInCategory(Guid categoryId, Geopoint location);
    Place GetNearestPlaceInChain(Guid chainId, Geopoint location);
}