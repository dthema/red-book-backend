using Domain.Models;

namespace ServerApplication.Services;

public interface ISettingsService
{
    Task SetCheckAround(Guid userId, bool checkAround);
    Task SetInterestingCategories(Guid userId, ICollection<Guid> categoriesIds);
    Task AddFavoritePlace(Guid userId, Guid placeId);
    Task RemoveFavoritePlace(Guid userId, Guid placeId);
    Task<bool> GetCheckAround(Guid userId);
    Task<ICollection<Category>> GetInterestingCategories(Guid userId);
    Task<ICollection<Place>> GetFavoritePlaces(Guid userId);
    Task<UserSettings> GetUserSettings(Guid userId);
}
