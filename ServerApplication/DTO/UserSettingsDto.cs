namespace ServerApplication.DTO;

public record UserSettingsDto(Guid Id, bool CheckAround, IEnumerable<CategoryDto> Categories, IEnumerable<PlaceDto> FavoritePlaces);