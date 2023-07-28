using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class PlacesChain
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Description Description { get; set; } = new ();
    public virtual ICollection<PlacesWithChains> PlacesWithChains { get; set; } = new HashSet<PlacesWithChains>();
}