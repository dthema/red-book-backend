using Domain.Models;
using ServerApplication.DTO;

namespace ServerApplication.Mappers;

public static class PlaceMapper
{
    public static PlaceDto AsDto(this Place place)
        => new PlaceDto(place.Id, place.Description, place.CategoryId, place.Location);
    
    public static Place AsEntity(this PlaceDto dto)
        => new Place
        {
            Id = dto.Id,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            Location = dto.Location
        };

    public static Place AsEntity(this UnregisteredPlaceDto dto)
        => new Place
        {
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            Location = dto.Location
        };
}