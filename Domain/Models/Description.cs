using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Description
{
    [MaxLength(32)]
    [Required]
    public string Name { get; set; }
    [MaxLength(32)]
    [Required]
    public string Title { get; set; }
    [Required]
    public string DescriptionText { get; set; }
    [Required]
    public string ImagePath { get; set; }
    [Required]
    public string AudioFilePath { get; set; }
}