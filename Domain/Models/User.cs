using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [MinLength(5), MaxLength(16)]
    [Required]
    public string Login { get; set; }
    [MinLength(6), MaxLength(32)]
    [Required]
    public string Password { get; set; }
    public virtual UserSettings Settings { get; set; } = new ();
}