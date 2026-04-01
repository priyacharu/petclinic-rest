using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicRest.Controllers;
using PetClinicRest.Data;
using PetClinicRest.DTOs;
using PetClinicRest.Mappings;
using PetClinicRest.Models;
using Xunit;

namespace PetClinicRest.Tests.Controllers;

public class OwnersControllerTests
{
    // ── Helpers ─────────────────────────────────────────────────────────────────

    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        return config.CreateMapper();
    }

    /// <summary>
    /// Each test gets its own isolated in-memory database.
    /// </summary>
    private static PetClinicDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<PetClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PetClinicDbContext(options);
    }

    private static OwnersController CreateController(PetClinicDbContext context)
        => new OwnersController(context, CreateMapper());

    // ── GetOwners ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetOwners_WithNoOwners_ReturnsOkWithEmptyList()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetOwners();

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var owners = ok.Value.Should().BeAssignableTo<IEnumerable<OwnerDto>>().Subject;
        owners.Should().BeEmpty();
    }

    [Fact]
    public async Task GetOwners_WithExistingOwners_ReturnsAllOwners()
    {
        // Arrange
        await using var context = CreateContext();
        context.Owners.AddRange(
            new Owner { FirstName = "John", LastName = "Doe",   Address = "123 Main St", City = "Springfield",  Telephone = "5551234" },
            new Owner { FirstName = "Jane", LastName = "Smith", Address = "456 Oak Ave", City = "Shelbyville",  Telephone = "5555678" }
        );
        await context.SaveChangesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetOwners();

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var owners = ok.Value.Should().BeAssignableTo<IEnumerable<OwnerDto>>().Subject;
        owners.Should().HaveCount(2);
    }

    // ── GetOwner ─────────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetOwner_WithExistingId_ReturnsOwner()
    {
        // Arrange
        await using var context = CreateContext();
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetOwner(owner.Id);

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var dto = ok.Value.Should().BeOfType<OwnerDto>().Subject;
        dto.FirstName.Should().Be("John");
        dto.LastName.Should().Be("Doe");
    }

    [Fact]
    public async Task GetOwner_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetOwner(999);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    // ── CreateOwner ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task CreateOwner_WithValidInput_ReturnsCreatedAtAction()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);
        var ownerDto = new OwnerDto
        {
            FirstName = "Alice",
            LastName  = "Martin",
            Address   = "789 Pine Rd",
            City      = "Shelbyville",
            Telephone = "5559012"
        };

        // Act
        var result = await sut.CreateOwner(ownerDto);

        // Assert
        var created = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        created.ActionName.Should().Be(nameof(OwnersController.GetOwner));
        var dto = created.Value.Should().BeOfType<OwnerDto>().Subject;
        dto.Id.Should().BeGreaterThan(0);
        dto.FirstName.Should().Be("Alice");
    }

    [Fact]
    public async Task CreateOwner_WithValidInput_PersistsOwnerToDatabase()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);
        var ownerDto = new OwnerDto { FirstName = "Bob", LastName = "Lee", Address = "1 Elm St", City = "Capital City", Telephone = "5550001" };

        // Act
        await sut.CreateOwner(ownerDto);

        // Assert
        var count = await context.Owners.CountAsync();
        count.Should().Be(1);
        var persisted = await context.Owners.FirstAsync();
        persisted.FirstName.Should().Be("Bob");
    }

    // ── UpdateOwner ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateOwner_WithExistingId_ReturnsNoContent()
    {
        // Arrange
        await using var context = CreateContext();
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var sut = CreateController(context);
        var updateDto = new OwnerDto
        {
            FirstName = "Johnny",
            LastName  = "Doe",
            Address   = "321 Elm Ave",
            City      = "Springfield",
            Telephone = "5554321"
        };

        // Act
        var result = await sut.UpdateOwner(owner.Id, updateDto);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        var updated = await context.Owners.FindAsync(owner.Id);
        updated!.FirstName.Should().Be("Johnny");
        updated.Telephone.Should().Be("5554321");
    }

    [Fact]
    public async Task UpdateOwner_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);
        var updateDto = new OwnerDto { FirstName = "Ghost", LastName = "Owner", Address = "N/A", City = "N/A", Telephone = "0000000" };

        // Act
        var result = await sut.UpdateOwner(999, updateDto);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    // ── DeleteOwner ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteOwner_WithExistingId_ReturnsNoContent()
    {
        // Arrange
        await using var context = CreateContext();
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.DeleteOwner(owner.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        var count = await context.Owners.CountAsync();
        count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteOwner_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);

        // Act
        var result = await sut.DeleteOwner(999);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    // ── GetOwnerPet ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetOwnerPet_WithValidOwnerAndPetIds_ReturnsPet()
    {
        // Arrange
        await using var context = CreateContext();
        var petType = new PetType { Name = "Cat" };
        context.PetTypes.Add(petType);
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var pet = new Pet { Name = "Whiskers", BirthDate = new DateTime(2020, 1, 1), OwnerId = owner.Id, PetTypeId = petType.Id };
        context.Pets.Add(pet);
        await context.SaveChangesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetOwnerPet(owner.Id, pet.Id);

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var dto = ok.Value.Should().BeOfType<PetDto>().Subject;
        dto.Name.Should().Be("Whiskers");
        dto.OwnerId.Should().Be(owner.Id);
    }

    [Fact]
    public async Task GetOwnerPet_WithNonExistentPetId_ReturnsNotFound()
    {
        // Arrange
        await using var context = CreateContext();
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetOwnerPet(owner.Id, 999);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetOwnerPet_WithPetBelongingToDifferentOwner_ReturnsNotFound()
    {
        // Arrange
        await using var context = CreateContext();
        var petType = new PetType { Name = "Dog" };
        context.PetTypes.Add(petType);
        var owner1 = new Owner { FirstName = "John", LastName = "Doe",   Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        var owner2 = new Owner { FirstName = "Jane", LastName = "Smith", Address = "456 Oak Ave", City = "Shelbyville", Telephone = "5555678" };
        context.Owners.AddRange(owner1, owner2);
        await context.SaveChangesAsync();
        var pet = new Pet { Name = "Rex", BirthDate = new DateTime(2019, 6, 15), OwnerId = owner2.Id, PetTypeId = petType.Id };
        context.Pets.Add(pet);
        await context.SaveChangesAsync();
        var sut = CreateController(context);

        // Act — requesting owner1's pets but pet belongs to owner2
        var result = await sut.GetOwnerPet(owner1.Id, pet.Id);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    // ── CreatePetForOwner ────────────────────────────────────────────────────────

    [Fact]
    public async Task CreatePetForOwner_WithValidOwnerId_ReturnsCreatedAtAction()
    {
        // Arrange
        await using var context = CreateContext();
        var petType = new PetType { Name = "Dog" };
        context.PetTypes.Add(petType);
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var sut = CreateController(context);
        var petDto = new PetDto { Name = "Buddy", BirthDate = new DateTime(2021, 3, 10), PetTypeId = petType.Id };

        // Act
        var result = await sut.CreatePetForOwner(owner.Id, petDto);

        // Assert
        var created = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        created.ActionName.Should().Be(nameof(OwnersController.GetOwnerPet));
        var dto = created.Value.Should().BeOfType<PetDto>().Subject;
        dto.Id.Should().BeGreaterThan(0);
        dto.Name.Should().Be("Buddy");
        dto.OwnerId.Should().Be(owner.Id);
    }

    [Fact]
    public async Task CreatePetForOwner_WithNonExistentOwnerId_ReturnsNotFound()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);
        var petDto = new PetDto { Name = "Ghost", BirthDate = new DateTime(2022, 1, 1), PetTypeId = 1 };

        // Act
        var result = await sut.CreatePetForOwner(999, petDto);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    // ── UpdateOwnerPet ───────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateOwnerPet_WithExistingOwnerAndPet_ReturnsNoContent()
    {
        // Arrange
        await using var context = CreateContext();
        var petType = new PetType { Name = "Cat" };
        context.PetTypes.Add(petType);
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var pet = new Pet { Name = "Whiskers", BirthDate = new DateTime(2020, 1, 1), OwnerId = owner.Id, PetTypeId = petType.Id };
        context.Pets.Add(pet);
        await context.SaveChangesAsync();
        var sut = CreateController(context);
        var updateDto = new PetDto { Name = "Mittens", BirthDate = new DateTime(2020, 1, 1), OwnerId = owner.Id, PetTypeId = petType.Id };

        // Act
        var result = await sut.UpdateOwnerPet(owner.Id, pet.Id, updateDto);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        var updated = await context.Pets.FindAsync(pet.Id);
        updated!.Name.Should().Be("Mittens");
    }

    [Fact]
    public async Task UpdateOwnerPet_WithNonExistentPet_ReturnsNotFound()
    {
        // Arrange
        await using var context = CreateContext();
        var owner = new Owner { FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Springfield", Telephone = "5551234" };
        context.Owners.Add(owner);
        await context.SaveChangesAsync();
        var sut = CreateController(context);
        var updateDto = new PetDto { Name = "Ghost", BirthDate = new DateTime(2022, 1, 1), PetTypeId = 1 };

        // Act
        var result = await sut.UpdateOwnerPet(owner.Id, 999, updateDto);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
