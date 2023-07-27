using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Geopoint
{
    public Geopoint(double latitude = 0, double longitude = 0)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
}