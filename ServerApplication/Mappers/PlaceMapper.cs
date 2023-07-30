using Domain.Models;
using ServerApplication.DTO;

namespace ServerApplication.Mappers;

public static class PlaceMapper
{
    public static PlaceDto AsDto(this Place place)
        => new PlaceDto(place.Id, place.Description, place.Category.AsDto(), place.Location);
}