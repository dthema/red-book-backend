using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Category : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [MaxLength(32)]
    [Required]
    public string Name { get; set; }
    [Required]
    public string IconFilePath { get; set; }
    public virtual ICollection<CategorySettings> CategorySettings { get; set; } = new List<CategorySettings>();
}