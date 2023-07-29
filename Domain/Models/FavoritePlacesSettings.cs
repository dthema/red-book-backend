namespace Domain.Models;

public class FavoritePlacesSettings
{
    public Guid PlaceId { get; set; }
    public Guid UserSettingsId { get; set; }
    public Place AssociatedPlace { get; set; }
    public UserSettings AssociatedSettings { get; set; }
}