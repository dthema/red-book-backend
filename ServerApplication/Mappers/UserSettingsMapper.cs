using Domain.Models;
using ServerApplication.DTO;

namespace ServerApplication.Mappers;

public static class UserSettingsMapper
{
    public static UserSettingsDto AsDto(this UserSettings settings)
        => new UserSettingsDto(
            settings.Id,
            settings.CheckAround,
            settings.CategorySettings.Select(x => x.AssociatedCategory.AsDto()),
            settings.FavoritePlacesSettings.Select(x => x.AssociatedPlace.AsDto()));
}