using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Category
{
    // public Category(Guid id, string name)
    // {
    //     Id = id;
    //     Name = name;
    // }
    //
    // protected Category() { }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
}