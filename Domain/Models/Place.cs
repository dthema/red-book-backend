using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Place : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    [Required]
    public Category Category { get; set; }
    public Description Description { get; set; } = new ();
    public Geopoint Location { get; set; } = new ();
    public virtual ICollection<PlacesWithChains> PlacesWithChains { get; set; } = new List<PlacesWithChains>();
    public virtual ICollection<FavoritePlacesSettings> FavoritePlacesSettings { get; set; } = new List<FavoritePlacesSettings>();
}