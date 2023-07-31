using Domain.Models;

namespace ServerApplication.DTO;

public record UnregisteredPlaceDto(Description Description, Guid CategoryId, Geopoint Location);