using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class User
{
    // public User(Guid id, string login, string password, UserSettings settings)
    // {
    //     Id = id;
    //     Login = login;
    //     Password = password;
    //     Settings = settings;
    // }
    //
    // protected User() { }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public virtual UserSettings Settings { get; set; }
}