using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class PlacesWithChains
{
    public Guid PlaceId { get; set; }
    public Guid PlacesChainId { get; set; }
    public Place AssociatedPlace { get; set; }
    public PlacesChain AssociatedChain { get; set; }
    [Range(0, int.MaxValue)]
    public int Order { get; set; }
}