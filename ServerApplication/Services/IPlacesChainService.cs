using Domain.Models;

namespace ServerApplication.Services;

public interface IPlacesChainService : ICrudService<PlacesChain>
{
    Task AddPlaceToChain(Guid chainId, Guid placeId, int order);
    Task RemovePlaceToChain(Guid chainId, Guid placeId);
}