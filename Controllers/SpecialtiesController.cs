using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicRest.Data;
using PetClinicRest.DTOs;
using PetClinicRest.Models;

namespace PetClinicRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialtiesController : ControllerBase
{
    private readonly PetClinicDbContext _context;
    private readonly IMapper _mapper;

    public SpecialtiesController(PetClinicDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all specialties
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecialtyDto>>> GetSpecialties()
    {
        var specialties = await _context.Specialties
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<SpecialtyDto>>(specialties));
    }

    /// <summary>
    /// Get specialty by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SpecialtyDto>> GetSpecialty(int id)
    {
        var specialty = await _context.Specialties
            .FirstOrDefaultAsync(s => s.Id == id);

        if (specialty == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<SpecialtyDto>(specialty));
    }

    /// <summary>
    /// Create a new specialty
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SpecialtyDto>> CreateSpecialty(SpecialtyDto specialtyDto)
    {
        var specialty = _mapper.Map<Specialty>(specialtyDto);
        _context.Specialties.Add(specialty);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSpecialty), new { id = specialty.Id }, _mapper.Map<SpecialtyDto>(specialty));
    }

    /// <summary>
    /// Update a specialty
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpecialty(int id, SpecialtyDto specialtyDto)
    {
        var specialty = await _context.Specialties.FindAsync(id);
        if (specialty == null)
        {
            return NotFound();
        }

        _mapper.Map(specialtyDto, specialty);

        _context.Specialties.Update(specialty);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a specialty
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecialty(int id)
    {
        var specialty = await _context.Specialties.FindAsync(id);
        if (specialty == null)
        {
            return NotFound();
        }

        _context.Specialties.Remove(specialty);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
