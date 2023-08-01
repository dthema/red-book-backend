using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class PlacesChain : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Description Description { get; set; } = new ();
    public virtual ICollection<PlacesWithChains> PlacesWithChains { get; set; } = new List<PlacesWithChains>();
}