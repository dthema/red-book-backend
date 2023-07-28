namespace Domain.Models;

public class PlacesWithChains
{
    public Guid PlaceId { get; set; }
    public Guid PlacesChainId { get; set; }
    public Place AssociatedPlace { get; set; }
    public PlacesChain AssociatedChain { get; set; }
    public int Order { get; set; }
}