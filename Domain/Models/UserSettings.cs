using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class UserSettings
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool CheckAround { get; set; }
    public virtual ICollection<CategorySettings> CategorySettings { get; set; } = new List<CategorySettings>();
    public virtual ICollection<FavoritePlacesSettings> FavoritePlacesSettings { get; set; } = new List<FavoritePlacesSettings>();
}