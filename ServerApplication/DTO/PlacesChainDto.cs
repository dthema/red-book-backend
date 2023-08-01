using Domain.Models;

namespace ServerApplication.DTO;

public record PlacesChainDto(Guid Id, Description Description, Guid CategoryId, IEnumerable<PlacesWithChainDto> PlacesWithChain);