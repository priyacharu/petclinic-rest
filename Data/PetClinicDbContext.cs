using Microsoft.EntityFrameworkCore;
using PetClinicRest.Models;

namespace PetClinicRest.Data;

public class PetClinicDbContext : DbContext
{
    public PetClinicDbContext(DbContextOptions<PetClinicDbContext> options) : base(options)
    {
    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetType> PetTypes { get; set; }
    public DbSet<Vet> Vets { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<VetSpecialty> VetSpecialties { get; set; }
    public DbSet<Visit> Visits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Owner
        modelBuilder.Entity<Owner>()
            .HasKey(o => o.Id);
        modelBuilder.Entity<Owner>()
            .Property(o => o.FirstName).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Owner>()
            .Property(o => o.LastName).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Owner>()
            .HasMany(o => o.Pets)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Pet
        modelBuilder.Entity<Pet>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Pet>()
            .Property(p => p.Name).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Pet>()
            .HasOne(p => p.Owner)
            .WithMany(o => o.Pets)
            .HasForeignKey(p => p.OwnerId);
        modelBuilder.Entity<Pet>()
            .HasOne(p => p.PetType)
            .WithMany(pt => pt.Pets)
            .HasForeignKey(p => p.PetTypeId);
        modelBuilder.Entity<Pet>()
            .HasMany(p => p.Visits)
            .WithOne(v => v.Pet)
            .HasForeignKey(v => v.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure PetType
        modelBuilder.Entity<PetType>()
            .HasKey(pt => pt.Id);
        modelBuilder.Entity<PetType>()
            .Property(pt => pt.Name).IsRequired().HasMaxLength(80);

        // Configure Vet
        modelBuilder.Entity<Vet>()
            .HasKey(v => v.Id);
        modelBuilder.Entity<Vet>()
            .Property(v => v.FirstName).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Vet>()
            .Property(v => v.LastName).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Vet>()
            .HasMany(v => v.Visits)
            .WithOne(v => v.Vet)
            .HasForeignKey(v => v.VetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Specialty
        modelBuilder.Entity<Specialty>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Specialty>()
            .Property(s => s.Name).IsRequired().HasMaxLength(80);

        // Configure VetSpecialty (many-to-many)
        modelBuilder.Entity<VetSpecialty>()
            .HasKey(vs => new { vs.VetId, vs.SpecialtyId });
        modelBuilder.Entity<VetSpecialty>()
            .HasOne(vs => vs.Vet)
            .WithMany(v => v.VetSpecialties)
            .HasForeignKey(vs => vs.VetId);
        modelBuilder.Entity<VetSpecialty>()
            .HasOne(vs => vs.Specialty)
            .WithMany(s => s.VetSpecialties)
            .HasForeignKey(vs => vs.SpecialtyId);

        // Configure Visit
        modelBuilder.Entity<Visit>()
            .HasKey(v => v.Id);
        modelBuilder.Entity<Visit>()
            .Property(v => v.Description).HasMaxLength(255);
        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Pet)
            .WithMany(p => p.Visits)
            .HasForeignKey(v => v.PetId);
        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Vet)
            .WithMany(v => v.Visits)
            .HasForeignKey(v => v.VetId);
    }
}
