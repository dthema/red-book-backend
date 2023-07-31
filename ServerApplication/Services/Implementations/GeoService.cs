using Domain;
using Domain.Models;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class GeoService : IGeoService
{
    private readonly ApplicationContext _appCtx;

    public GeoService(ApplicationContext appCtx)
    {
        _appCtx = appCtx;
    }

    public Place GetNearestPlace(Geopoint location)
    {
        return GetNearestPlace(location, _appCtx.Places);
    }

    public Place GetNearestPlaceInCategory(Guid categoryId, Geopoint location)
    {
        return GetNearestPlace(location, _appCtx.Places.Where(x => x.CategoryId.Equals(categoryId)));
    }

    public Place GetNearestPlaceInChain(Guid chainId, Geopoint location)
    {
        return GetNearestPlace(location, _appCtx.Places
            .Include(x => x.PlacesWithChains)
            .Where(x => x.PlacesWithChains.Any(c => c.PlacesChainId.Equals(chainId))));
    }

    public PlacesChain GetNearestChain(Geopoint location)
    {
        return GetNearestPlace(location, _appCtx.Places
                .Include(x => x.PlacesWithChains)
                .Where(x => x.PlacesWithChains.Any(c => c.Order.Equals(1))))
            .PlacesWithChains
            .First(x => x.Order.Equals(1)).AssociatedChain;
    }

    private static Place GetNearestPlace(Geopoint location, IEnumerable<Place> dbSet)
    {
        var coordinate = new GeoCoordinate(location.Latitude, location.Longitude);
        var minDistance = double.MaxValue;
        Place? nearestPlace = null;
        
        foreach (var place in dbSet)
        {
            var distance =
                coordinate.GetDistanceTo(new GeoCoordinate(place.Location.Latitude, place.Location.Longitude));
            if (distance.CompareTo(minDistance) >= 0) continue;
            minDistance = distance;
            nearestPlace = place;
        }

        return nearestPlace ?? throw new ArgumentException();
    }
}