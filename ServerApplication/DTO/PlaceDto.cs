using Domain.Models;

namespace ServerApplication.DTO;

public record PlaceDto(Guid Id, Description Description, Guid CategoryId, Geopoint Location);