using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class UserSettings
{
    // public UserSettings(Guid id, bool checkAround)
    // {
    //     Id = id;
    //     CheckAround = checkAround;
    //     Categories = new List<Category>();
    // }
    //
    // protected UserSettings() { }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public bool CheckAround { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
}