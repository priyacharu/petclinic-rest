using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicRest.Data;
using PetClinicRest.DTOs;
using PetClinicRest.Models;

namespace PetClinicRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VetsController : ControllerBase
{
    private readonly PetClinicDbContext _context;
    private readonly IMapper _mapper;

    public VetsController(PetClinicDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all veterinarians
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VetDto>>> GetVets()
    {
        var vets = await _context.Vets
            .Include(v => v.VetSpecialties)
            .ThenInclude(vs => vs.Specialty)
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<VetDto>>(vets));
    }

    /// <summary>
    /// Get veterinarian by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VetDto>> GetVet(int id)
    {
        var vet = await _context.Vets
            .Include(v => v.VetSpecialties)
            .ThenInclude(vs => vs.Specialty)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vet == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<VetDto>(vet));
    }

    /// <summary>
    /// Create a new veterinarian
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<VetDto>> CreateVet(VetDto vetDto)
    {
        var vet = _mapper.Map<Vet>(vetDto);
        _context.Vets.Add(vet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVet), new { id = vet.Id }, _mapper.Map<VetDto>(vet));
    }

    /// <summary>
    /// Update a veterinarian
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVet(int id, VetDto vetDto)
    {
        var vet = await _context.Vets.FindAsync(id);
        if (vet == null)
        {
            return NotFound();
        }

        _mapper.Map(vetDto, vet);
        vet.UpdatedAt = DateTime.UtcNow;

        _context.Vets.Update(vet);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a veterinarian
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVet(int id)
    {
        var vet = await _context.Vets.FindAsync(id);
        if (vet == null)
        {
            return NotFound();
        }

        _context.Vets.Remove(vet);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
