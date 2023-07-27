using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Place
{
    // public Place(Guid id)
    // {
        // Id = id;
        // Description = description;
        // Location = location;
    // }
    //
    // protected Place() { }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Description Description { get; set; }
    public Geopoint Location { get; set; }
}