using Domain.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=red-book;Username=postgres;Password=postgres");
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserSettings> Settings { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<PlacesChain> PlacesChains { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<UserSettings>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Category>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Place>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Place>()
            .OwnsOne(x => x.Location);
        modelBuilder.Entity<PlacesChain>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<User>()
            .HasOne(x => x.Settings);
        modelBuilder.Entity<UserSettings>()
            .HasMany(x => x.Categories);

        modelBuilder.Entity<Place>()
            .OwnsOne(x => x.Description);
        modelBuilder.Entity<PlacesChain>()
            .HasMany(x => x.Places);
        modelBuilder.Entity<PlacesChain>()
            .OwnsOne(x => x.Description);
    }
}