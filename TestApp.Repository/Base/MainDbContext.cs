using Microsoft.EntityFrameworkCore;
using TestApp.Data.Contracts.Models;

namespace TestApp.Data;

public class MainDbContext : DbContext
{
    public DbSet<Company> Companies { get; private set; }

    public DbSet<User> Users { get; private set; }

    public MainDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUsers(modelBuilder);
        ConfigureCompanies(modelBuilder);
    }

    private static void ConfigureUsers(ModelBuilder modelBuilder)
    {
        var entityTypeBuilder
            = modelBuilder.Entity<User>();

        entityTypeBuilder
            .ToTable("Users", "public")
            .HasKey(p => p.Id);
        entityTypeBuilder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        entityTypeBuilder.HasIndex(p => p.Email).IsUnique();
        entityTypeBuilder.HasOne(p => p.Company)
            .WithMany(x => x.Users)
            .HasForeignKey(p => p.CompanyId)
            .HasPrincipalKey(p => p.Id);
    }

    private static void ConfigureCompanies(ModelBuilder modelBuilder)
    {
        var entityTypeBuilder
            = modelBuilder.Entity<Company>();
        entityTypeBuilder
            .ToTable("Companies", "public")
            .HasKey(p => p.Id);
        entityTypeBuilder.HasIndex(p => p.CompanyName).IsUnique();
        entityTypeBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}

