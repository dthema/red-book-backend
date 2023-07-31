using Domain.Models;
using ServerApplication.DTO;

namespace ServerApplication.Mappers;

public static class PlaceChainMapper
{
    public static PlacesChainDto AsDto(this PlacesChain placesChain)
        => new PlacesChainDto(placesChain.Id, placesChain.Description, placesChain.PlacesWithChains.Select(x => x.AsDto()));
    
    public static PlacesChain AsEntity(this PlacesChainDto dto)
        => new PlacesChain
        {
            Id = dto.Id,
            Description = dto.Description,
            PlacesWithChains = dto.PlacesWithChain.Select(x => x.AsEntity()).ToList()
        };

    public static PlacesChain AsEntity(this UnregisteredPlacesChainDto dto)
        => new PlacesChain
        {
            Description = dto.Description,
        };
    
    public static PlacesChain AsEntity(this UpdatedPlacesChainDto dto)
        => new PlacesChain
        {
            Id = dto.Id,
            Description = dto.Description,
        };
    
    public static PlacesWithChainDto AsDto(this PlacesWithChains placesWithChains)
        => new PlacesWithChainDto(placesWithChains.PlacesChainId, placesWithChains.PlaceId, placesWithChains.Order);

    public static PlacesWithChains AsEntity(this PlacesWithChainDto dto)
        => new PlacesWithChains
        {
            PlacesChainId = dto.ChainId,
            PlaceId = dto.PlaceId
        };
}