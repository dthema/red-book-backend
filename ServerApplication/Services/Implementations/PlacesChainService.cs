using Domain;
using Domain.Models;

namespace ServerApplication.Services.Implementations;

public class PlacesChainService : CrudService<PlacesChain>, IPlacesChainService
{
    public PlacesChainService(ApplicationContext appCtx)
        : base(appCtx) { }

    public async Task AddPlaceToChain(Guid chainId, Guid placeId, int order)
    {
        await _appCtx.PlacesWithChains.AddAsync(new PlacesWithChains
        {
            PlaceId = placeId,
            PlacesChainId = chainId,
            Order = order
        });
        await _appCtx.SaveChangesAsync();
    }

    public async Task RemovePlaceToChain(Guid chainId, Guid placeId)
    {
        var link = await _appCtx.PlacesWithChains.FindAsync(placeId, chainId)
                   ?? throw new ArgumentException();
        _appCtx.PlacesWithChains.Remove(link);
        await _appCtx.SaveChangesAsync();
    }
}