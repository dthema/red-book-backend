using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerApplication.Models;

namespace Domain;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserSettings> Settings { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<PlacesChain> PlacesChains { get; set; }
    public DbSet<FavoritePlacesSettings> FavoritePlacesSettings { get; set; }
    public DbSet<CategorySettings> CategorySettings { get; set; }
    public DbSet<PlacesWithChains> PlacesWithChains { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CategorySettings>()
            .HasKey(x => new { x.UserSettingsId, x.CategoryId });
        modelBuilder.Entity<CategorySettings>()
            .HasOne(x => x.AssociatedSettings)
            .WithMany(x => x.CategorySettings)
            .HasForeignKey(x => x.UserSettingsId);
        modelBuilder.Entity<CategorySettings>()
            .HasOne(x => x.AssociatedCategory)
            .WithMany(x => x.CategorySettings)
            .HasForeignKey(x => x.CategoryId);

        modelBuilder.Entity<PlacesWithChains>()
            .HasKey(x => new { x.PlaceId, x.PlacesChainId });
        modelBuilder.Entity<PlacesWithChains>()
            .HasOne(x => x.AssociatedPlace)
            .WithMany(x => x.PlacesWithChains)
            .HasForeignKey(x => x.PlaceId);
        modelBuilder.Entity<PlacesWithChains>()
            .HasOne(x => x.AssociatedChain)
            .WithMany(x => x.PlacesWithChains)
            .HasForeignKey(x => x.PlacesChainId);

        modelBuilder.Entity<FavoritePlacesSettings>()
            .HasKey(x => new { x.UserSettingsId, x.PlaceId });
        modelBuilder.Entity<FavoritePlacesSettings>()
            .HasOne(x => x.AssociatedSettings)
            .WithMany(x => x.FavoritePlacesSettings)
            .HasForeignKey(x => x.UserSettingsId);
        modelBuilder.Entity<FavoritePlacesSettings>()
            .HasOne(x => x.AssociatedPlace)
            .WithMany(x => x.FavoritePlacesSettings)
            .HasForeignKey(x => x.PlaceId);
        
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Login)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasOne(x => x.Settings)
            .WithOne(x => x.User)
            .HasForeignKey<UserSettings>(x => x.UserId)
            .IsRequired();

        modelBuilder.Entity<UserSettings>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Category>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Category>()
            .HasIndex(x => x.Name)
            .IsUnique();
        
        modelBuilder.Entity<Place>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Place>()
            .HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId);
        modelBuilder.Entity<Place>()
            .OwnsOne(x => x.Location);
        modelBuilder.Entity<Place>()
            .OwnsOne(x => x.Description);
        
        modelBuilder.Entity<PlacesChain>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<PlacesChain>()
            .OwnsOne(x => x.Description);
    }
}