using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicRest.Data;
using PetClinicRest.DTOs;
using PetClinicRest.Models;

namespace PetClinicRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly PetClinicDbContext _context;
    private readonly IMapper _mapper;

    public PetsController(PetClinicDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all pets
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetDto>>> GetPets()
    {
        var pets = await _context.Pets
            .Include(p => p.PetType)
            .Include(p => p.Visits)
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<PetDto>>(pets));
    }

    /// <summary>
    /// Get pet by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PetDto>> GetPet(int id)
    {
        var pet = await _context.Pets
            .Include(p => p.PetType)
            .Include(p => p.Visits)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pet == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PetDto>(pet));
    }

    /// <summary>
    /// Create a new pet
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PetDto>> CreatePet(PetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, _mapper.Map<PetDto>(pet));
    }

    /// <summary>
    /// Update a pet
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(int id, PetDto petDto)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null)
        {
            return NotFound();
        }

        _mapper.Map(petDto, pet);
        pet.UpdatedAt = DateTime.UtcNow;

        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a pet
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(int id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null)
        {
            return NotFound();
        }

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
