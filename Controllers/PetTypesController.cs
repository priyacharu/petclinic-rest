using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicRest.Data;
using PetClinicRest.DTOs;
using PetClinicRest.Models;

namespace PetClinicRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetTypesController : ControllerBase
{
    private readonly PetClinicDbContext _context;
    private readonly IMapper _mapper;

    public PetTypesController(PetClinicDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all pet types
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetTypeDto>>> GetPetTypes()
    {
        var petTypes = await _context.PetTypes
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<PetTypeDto>>(petTypes));
    }

    /// <summary>
    /// Get pet type by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PetTypeDto>> GetPetType(int id)
    {
        var petType = await _context.PetTypes
            .FirstOrDefaultAsync(pt => pt.Id == id);

        if (petType == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PetTypeDto>(petType));
    }

    /// <summary>
    /// Create a new pet type
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PetTypeDto>> CreatePetType(PetTypeDto petTypeDto)
    {
        var petType = _mapper.Map<PetType>(petTypeDto);
        _context.PetTypes.Add(petType);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPetType), new { id = petType.Id }, _mapper.Map<PetTypeDto>(petType));
    }

    /// <summary>
    /// Update a pet type
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePetType(int id, PetTypeDto petTypeDto)
    {
        var petType = await _context.PetTypes.FindAsync(id);
        if (petType == null)
        {
            return NotFound();
        }

        _mapper.Map(petTypeDto, petType);

        _context.PetTypes.Update(petType);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a pet type
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePetType(int id)
    {
        var petType = await _context.PetTypes.FindAsync(id);
        if (petType == null)
        {
            return NotFound();
        }

        _context.PetTypes.Remove(petType);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
