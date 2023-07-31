using Domain.Models;

namespace ServerApplication.DTO;

public record PlacesChainDto(Guid Id, Description Description, IEnumerable<PlacesWithChainDto> PlacesWithChain);