using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Place
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public Category Category { get; set; }
    public Description Description { get; set; } = new ();
    public Geopoint Location { get; set; } = new ();
    public virtual ICollection<PlacesWithChains> PlacesWithChains { get; set; } = new HashSet<PlacesWithChains>();
}