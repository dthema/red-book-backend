using Domain.Models;

namespace ServerApplication.DTO;

public record PlaceDto(Guid Id, Description Description, CategoryDto Category, Geopoint Location);