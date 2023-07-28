namespace Domain.Models;

public class CategorySettings
{
    public Guid CategoryId { get; set; }
    public Guid UserSettingsId { get; set; }
    public Category AssociatedCategory { get; set; }
    public UserSettings AssociatedSettings { get; set; }
}