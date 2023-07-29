using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class PlacesChainService : IPlacesChainService
{
    private readonly ApplicationContext _appCtx;

    public PlacesChainService(ApplicationContext appCtx)
    {
        _appCtx = appCtx;
    }

    public async Task<PlacesChain> Add(PlacesChain entity)
    {
        var placesChain = await _appCtx.PlacesChains.AddAsync(entity);
        await _appCtx.SaveChangesAsync();
        return placesChain.Entity;
    }

    public async Task<PlacesChain> Update(PlacesChain entity)
    {
        var placesChain = _appCtx.PlacesChains.Update(entity).Entity;
        await _appCtx.SaveChangesAsync();
        return placesChain;
    }

    public async Task<bool> Delete(PlacesChain entity)
    {
        _appCtx.PlacesChains.Remove(entity);
        await _appCtx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var placesChain = new PlacesChain { Id = id };
        _appCtx.PlacesChains.Attach(placesChain);
        _appCtx.PlacesChains.Remove(placesChain);
        await _appCtx.SaveChangesAsync();
        return true;
    }

    public async Task<PlacesChain> Get(Guid id)
    {
        return await Find(id) ?? throw new InvalidOperationException();
    }

    public Task<PlacesChain?> Find(Guid id)
    {
        return _appCtx.PlacesChains.FirstOrDefaultAsync(placesChain => placesChain.Id.Equals(id));
    }

    public Task<List<PlacesChain>> GetAll()
    {
        return _appCtx.PlacesChains.ToListAsync();
    }

    public async Task AddPlaceToChain(Guid chainId, Guid placeId, int order)
    {
        await _appCtx.PlacesWithChains.AddAsync(new PlacesWithChains
        {
            PlaceId = placeId,
            PlacesChainId = chainId,
            Order = order
        });
    }

    public async Task RemovePlaceToChain(Guid chainId, Guid placeId)
    {
        var link = await _appCtx.PlacesWithChains.FindAsync(placeId, chainId)
                   ?? throw new ArgumentException();
        _appCtx.PlacesWithChains.Remove(link);
    }
}