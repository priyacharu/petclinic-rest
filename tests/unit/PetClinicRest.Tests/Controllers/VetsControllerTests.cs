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

public class VetsControllerTests
{
    // ── Helpers ─────────────────────────────────────────────────────────────────

    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        return config.CreateMapper();
    }

    private static PetClinicDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<PetClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PetClinicDbContext(options);
    }

    private static VetsController CreateController(PetClinicDbContext context)
        => new VetsController(context, CreateMapper());

    private static void SeedVetsWithSpecialties(PetClinicDbContext context)
    {
        var radiology = new Specialty { Id = 1, Name = "radiology" };
        var surgery = new Specialty { Id = 2, Name = "surgery" };

        context.Specialties.AddRange(radiology, surgery);

        var vet1 = new Vet { Id = 1, FirstName = "James", LastName = "Carter" };
        var vet2 = new Vet { Id = 2, FirstName = "Helen", LastName = "Leary" };
        var vet3 = new Vet { Id = 3, FirstName = "Linda", LastName = "Douglas" };

        context.Vets.AddRange(vet1, vet2, vet3);

        context.VetSpecialties.AddRange(
            new VetSpecialty { VetId = 1, SpecialtyId = 1 }, // James -> radiology
            new VetSpecialty { VetId = 2, SpecialtyId = 2 }, // Helen -> surgery
            new VetSpecialty { VetId = 3, SpecialtyId = 1 }, // Linda -> radiology
            new VetSpecialty { VetId = 3, SpecialtyId = 2 }  // Linda -> surgery
        );

        context.SaveChanges();
    }

    // ── GetVets without specialty filter ─────────────────────────────────────────

    [Fact]
    public async Task GetVets_WithNoSpecialtyParam_ReturnsAllVets()
    {
        // Arrange
        await using var context = CreateContext();
        SeedVetsWithSpecialties(context);
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets(null);

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetVets_WithNoVetsInDatabase_ReturnsEmptyList()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets(null);

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().BeEmpty();
    }

    // ── GetVets with specialty filter ────────────────────────────────────────────

    [Fact]
    public async Task GetVets_WithMatchingSpecialty_ReturnsFilteredVets()
    {
        // Arrange
        await using var context = CreateContext();
        SeedVetsWithSpecialties(context);
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets("radiology");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(2);
        vets.Select(v => v.FirstName).Should().Contain("James").And.Contain("Linda");
    }

    [Fact]
    public async Task GetVets_WithSpecialtyCaseInsensitive_ReturnsFilteredVets()
    {
        // Arrange
        await using var context = CreateContext();
        SeedVetsWithSpecialties(context);
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets("Radiology");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetVets_WithNonMatchingSpecialty_ReturnsEmptyList()
    {
        // Arrange
        await using var context = CreateContext();
        SeedVetsWithSpecialties(context);
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets("dentistry");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().BeEmpty();
    }

    [Fact]
    public async Task GetVets_WithEmptySpecialty_ReturnsAllVets()
    {
        // Arrange
        await using var context = CreateContext();
        SeedVetsWithSpecialties(context);
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets("");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetVets_WithWhitespaceSpecialty_ReturnsAllVets()
    {
        // Arrange
        await using var context = CreateContext();
        SeedVetsWithSpecialties(context);
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets("   ");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(3);
    }
}
