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
        var settings = await GeTSettings(userId);
        var categories = settings.CategorySettings.Select(x => x.AssociatedCategory);
        var usedCategories = new List<Category>();
        var unusedCategories = new List<Category>();

        foreach (var category in _appCtx.Categories.ToList())
        {
            if (categoriesIds.Contains(category.Id) && !categories.Contains(category))
                usedCategories.Add(category);
            if (!categoriesIds.Contains(category.Id) && categories.Contains(category))
                unusedCategories.Add(category);
        }

        foreach (var link in unusedCategories
                     .Select(unusedCategory => _appCtx.Find<CategorySettings>(settings.Id, unusedCategory.Id))
                      ?? throw new ArgumentException())
        {
            _appCtx.Remove(link);
        }

        foreach (var usedCategory in usedCategories)
        {
            _appCtx.Add(new CategorySettings
            {
                UserSettingsId = settings.Id,
                CategoryId = usedCategory.Id
            });
        }

        _appCtx.Update(settings);
        await _appCtx.SaveChangesAsync();
    }

    public async Task<bool> GetCheckAround(Guid userId)
    {
        return (await GeTSettings(userId)).CheckAround;
    }

    public async Task<ICollection<Category>> GetInterestingCategories(Guid userId)
    {
        return (await GeTSettings(userId)).CategorySettings.Select(x => x.AssociatedCategory).ToList();
    }

    private async Task<UserSettings> GeTSettings(Guid userId)
    {
        return await _appCtx.Settings
            .Include(x => x.CategorySettings)
            .ThenInclude(x => x.AssociatedCategory)
            .FirstOrDefaultAsync(x => x.UserId.Equals(userId)) ?? throw new ArgumentException();
    }
}