namespace ServerApplication.DTO;

public record UserFavoritePlacesDto(Guid UserId, IEnumerable<PlaceDto> PlacesIds);