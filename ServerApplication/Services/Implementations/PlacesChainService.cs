using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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

    public new Task<PlacesChain?> Find(Guid id)
    {
        return _appCtx.PlacesChains
            .Include(x => x.PlacesWithChains)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public new Task<List<PlacesChain>> GetAll()
    {
        return _appCtx.PlacesChains
            .Include(x => x.PlacesWithChains)
            .ToListAsync();
    }
}