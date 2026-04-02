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

    /// <summary>
    /// Seeds two vets: one with "radiology", one with "surgery".
    /// Returns the seeded context (caller owns disposal).
    /// </summary>
    private static async Task<PetClinicDbContext> SeedVetsWithSpecialtiesAsync()
    {
        var context = CreateContext();

        var radiology = new Specialty { Name = "radiology" };
        var surgery   = new Specialty { Name = "surgery" };
        context.Specialties.AddRange(radiology, surgery);

        var vetJames = new Vet { FirstName = "James", LastName = "Carter" };
        var vetHelen = new Vet { FirstName = "Helen", LastName = "Leary" };
        context.Vets.AddRange(vetJames, vetHelen);

        await context.SaveChangesAsync();

        context.Set<VetSpecialty>().AddRange(
            new VetSpecialty { VetId = vetJames.Id, SpecialtyId = radiology.Id },
            new VetSpecialty { VetId = vetHelen.Id, SpecialtyId = surgery.Id }
        );
        await context.SaveChangesAsync();

        return context;
    }

    // ── GetVets (no filter) ──────────────────────────────────────────────────────

    [Fact]
    public async Task GetVets_WithNoSpecialtyParam_ReturnsAllVets()
    {
        // Arrange
        await using var context = await SeedVetsWithSpecialtiesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets();

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetVets_WithEmptySpecialtyParam_ReturnsAllVets()
    {
        // Arrange
        await using var context = await SeedVetsWithSpecialtiesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets(specialty: "");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetVets_WithWhitespaceSpecialtyParam_ReturnsAllVets()
    {
        // Arrange
        await using var context = await SeedVetsWithSpecialtiesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets(specialty: "   ");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(2);
    }

    // ── GetVets (with filter) ────────────────────────────────────────────────────

    [Fact]
    public async Task GetVets_WithMatchingSpecialty_ReturnsOnlyMatchingVets()
    {
        // Arrange
        await using var context = await SeedVetsWithSpecialtiesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets(specialty: "radiology");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject.ToList();
        vets.Should().HaveCount(1);
        vets[0].FirstName.Should().Be("James");
        vets[0].LastName.Should().Be("Carter");
    }

    [Fact]
    public async Task GetVets_WithMatchingSpecialtyDifferentCase_ReturnsOnlyMatchingVets()
    {
        // Arrange
        await using var context = await SeedVetsWithSpecialtiesAsync();
        var sut = CreateController(context);

        // Act — "Radiology" (uppercase R) should match "radiology"
        var result = await sut.GetVets(specialty: "Radiology");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetVets_WithNonMatchingSpecialty_ReturnsEmptyArray()
    {
        // Arrange
        await using var context = await SeedVetsWithSpecialtiesAsync();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets(specialty: "dentistry");

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().BeEmpty();
    }

    [Fact]
    public async Task GetVets_WithNoVetsInDatabase_ReturnsEmptyArray()
    {
        // Arrange
        await using var context = CreateContext();
        var sut = CreateController(context);

        // Act
        var result = await sut.GetVets();

        // Assert
        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var vets = ok.Value.Should().BeAssignableTo<IEnumerable<VetDto>>().Subject;
        vets.Should().BeEmpty();
    }
}
