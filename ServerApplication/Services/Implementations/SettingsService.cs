using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class SettingsService : ISettingsService
{
    private readonly ApplicationContext _appCtx;

    public SettingsService(ApplicationContext appCtx)
    {
        _appCtx = appCtx;
    }

    public async Task SetCheckAround(Guid userId, bool checkAround)
    {
        var settings = await GeTSettings(userId);
        settings.CheckAround = checkAround;
        _appCtx.Update(settings);
        await _appCtx.SaveChangesAsync();
    }

    public async Task SetInterestingCategories(Guid userId, ICollection<Guid> categoriesIds)
    {
        var settings = await GeTSettingsWithCategories(userId);
        var categories = await _appCtx.Categories.Where(x => categoriesIds.Contains(x.Id)).ToListAsync();
        var settingsCategories = settings.CategorySettings.Select(x => x.AssociatedCategory).ToList();

        var transaction = await _appCtx.Database.BeginTransactionAsync();
        
        foreach (var category in settingsCategories)
        {
            if (!categoriesIds.Contains(category.Id))
            {
                var link = await _appCtx.CategorySettings.FindAsync(settings.Id, category.Id)
                           ?? throw new ArgumentException();
                _appCtx.CategorySettings.Remove(link);
            }
            else
            {
                categories.Remove(category);
            }
        }

        foreach (var category in categories)
        {
            await _appCtx.CategorySettings.AddAsync(new CategorySettings
            {
                UserSettingsId = settings.Id,
                CategoryId = category.Id
            });
        }

        _appCtx.Update(settings);
        await _appCtx.SaveChangesAsync();
        
        await transaction.CommitAsync();
    }

    public async Task AddFavoritePlace(Guid userId, Guid placeId)
    {   
        var settings = await GeTSettings(userId);
        await _appCtx.AddAsync(new FavoritePlacesSettings
        {
            UserSettingsId = settings.Id,
            PlaceId = placeId
        });
        await _appCtx.SaveChangesAsync();
    }

    public async Task RemoveFavoritePlace(Guid userId, Guid placeId)
    {
        var settings = await GeTSettings(userId);
        _appCtx.Remove(new FavoritePlacesSettings
        {
            UserSettingsId = settings.Id,
            PlaceId = placeId
        });
        await _appCtx.SaveChangesAsync();
    }

    public async Task<bool> GetCheckAround(Guid userId)
    {
        return (await GeTSettings(userId)).CheckAround;
    }

    public async Task<ICollection<Category>> GetInterestingCategories(Guid userId)
    {
        
        return (await GeTSettingsWithCategories(userId)).CategorySettings
            .Select(x => x.AssociatedCategory).ToList();
    }

    public async Task<ICollection<Place>> GetFavoritePlaces(Guid userId)
    {
        var settings = await _appCtx.Settings
            .Include(x => x.FavoritePlacesSettings)
            .ThenInclude(x => x.AssociatedPlace)
            .FirstOrDefaultAsync(x => x.UserId.Equals(userId)) ?? throw new ArgumentException();
        return settings.FavoritePlacesSettings.Select(x => x.AssociatedPlace).ToList();
    }

    public async Task<UserSettings> GetUserSettings(Guid userId)
    {
        return await _appCtx.Settings
            .Include(x => x.CategorySettings)
            .ThenInclude(x => x.AssociatedCategory)
            .Include(x => x.FavoritePlacesSettings)
            .ThenInclude(x => x.AssociatedPlace)
            .ThenInclude(x => x.Category)
            .FirstOrDefaultAsync(x => x.UserId.Equals(userId)) ?? throw new ArgumentException();
    }

    private async Task<UserSettings> GeTSettings(Guid userId)
    {
        return await _appCtx.Settings
            .FirstOrDefaultAsync(x => x.UserId.Equals(userId)) ?? throw new ArgumentException();
    }

    private async Task<UserSettings> GeTSettingsWithCategories(Guid userId)
    {
        return await _appCtx.Settings
            .Include(x => x.CategorySettings)
            .ThenInclude(x => x.AssociatedCategory)
            .FirstOrDefaultAsync(x => x.UserId.Equals(userId)) ?? throw new ArgumentException();
    }
}