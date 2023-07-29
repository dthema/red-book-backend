using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class PlaceService : IPlaceService
{
    private readonly ApplicationContext _appCtx;

    public PlaceService(ApplicationContext appCtx)
    {
        _appCtx = appCtx;
    }

    public async Task<Place> Add(Place entity)
    {
        var place = await _appCtx.Places.AddAsync(entity);
        await _appCtx.SaveChangesAsync();
        return place.Entity;
    }

    public async Task<Place> Update(Place entity)
    {
        var place = _appCtx.Places.Update(entity).Entity;
        await _appCtx.SaveChangesAsync();
        return place;
    }

    public async Task<bool> Delete(Place entity)
    {
        _appCtx.Places.Remove(entity);
        await _appCtx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var place = new Place { Id = id };
        _appCtx.Places.Attach(place);
        _appCtx.Places.Remove(place);
        await _appCtx.SaveChangesAsync();
        return true;
    }

    public async Task<Place> Get(Guid id)
    {
        return await Find(id) ?? throw new InvalidOperationException();
    }

    public Task<Place?> Find(Guid id)
    {
        return _appCtx.Places.FirstOrDefaultAsync(place => place.Id.Equals(id));
    }

    public Task<List<Place>> GetAll()
    {
        return _appCtx.Places.ToListAsync();
    }
}