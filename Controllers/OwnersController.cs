using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicRest.Data;
using PetClinicRest.DTOs;
using PetClinicRest.Models;

namespace PetClinicRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly PetClinicDbContext _context;
    private readonly IMapper _mapper;

    public OwnersController(PetClinicDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all owners
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetOwners()
    {
        var owners = await _context.Owners
            .Include(o => o.Pets)
            .ThenInclude(p => p.PetType)
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<OwnerDto>>(owners));
    }

    /// <summary>
    /// Get owner by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerDto>> GetOwner(int id)
    {
        var owner = await _context.Owners
            .Include(o => o.Pets)
            .ThenInclude(p => p.PetType)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (owner == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<OwnerDto>(owner));
    }

    /// <summary>
    /// Create a new owner
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<OwnerDto>> CreateOwner(OwnerDto ownerDto)
    {
        var owner = _mapper.Map<Owner>(ownerDto);
        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOwner), new { id = owner.Id }, _mapper.Map<OwnerDto>(owner));
    }

    /// <summary>
    /// Update an owner
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOwner(int id, OwnerDto ownerDto)
    {
        var owner = await _context.Owners.FindAsync(id);
        if (owner == null)
        {
            return NotFound();
        }

        owner.FirstName = ownerDto.FirstName;
        owner.LastName = ownerDto.LastName;
        owner.Address = ownerDto.Address;
        owner.City = ownerDto.City;
        owner.Telephone = ownerDto.Telephone;
        owner.UpdatedAt = DateTime.UtcNow;

        _context.Owners.Update(owner);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete an owner
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOwner(int id)
    {
        var owner = await _context.Owners.FindAsync(id);
        if (owner == null)
        {
            return NotFound();
        }

        _context.Owners.Remove(owner);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Get a pet by owner ID and pet ID
    /// </summary>
    [HttpGet("{ownerId}/pets/{petId}")]
    public async Task<ActionResult<PetDto>> GetOwnerPet(int ownerId, int petId)
    {
        var pet = await _context.Pets
            .Include(p => p.PetType)
            .Include(p => p.Visits)
            .FirstOrDefaultAsync(p => p.Id == petId && p.OwnerId == ownerId);

        if (pet == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PetDto>(pet));
    }

    /// <summary>
    /// Add a new pet to an owner
    /// </summary>
    [HttpPost("{ownerId}/pets")]
    public async Task<ActionResult<PetDto>> CreatePetForOwner(int ownerId, PetDto petDto)
    {
        var owner = await _context.Owners.FindAsync(ownerId);
        if (owner == null)
        {
            return NotFound();
        }

        var pet = _mapper.Map<Pet>(petDto);
        pet.OwnerId = ownerId;
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOwnerPet), new { ownerId = ownerId, petId = pet.Id }, _mapper.Map<PetDto>(pet));
    }

    /// <summary>
    /// Update a pet for an owner
    /// </summary>
    [HttpPut("{ownerId}/pets/{petId}")]
    public async Task<IActionResult> UpdateOwnerPet(int ownerId, int petId, PetDto petDto)
    {
        var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == petId && p.OwnerId == ownerId);
        if (pet == null)
        {
            return NotFound();
        }

        pet.Name = petDto.Name;
        pet.BirthDate = petDto.BirthDate;
        pet.PetTypeId = petDto.PetTypeId;
        pet.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
