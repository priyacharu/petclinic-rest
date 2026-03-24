using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicRest.Data;
using PetClinicRest.DTOs;
using PetClinicRest.Models;

namespace PetClinicRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly PetClinicDbContext _context;
    private readonly IMapper _mapper;

    public VisitsController(PetClinicDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all visits
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisitDto>>> GetVisits()
    {
        var visits = await _context.Visits
            .Include(v => v.Vet)
            .Include(v => v.Pet)
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<VisitDto>>(visits));
    }

    /// <summary>
    /// Get visit by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VisitDto>> GetVisit(int id)
    {
        var visit = await _context.Visits
            .Include(v => v.Vet)
            .Include(v => v.Pet)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (visit == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<VisitDto>(visit));
    }

    /// <summary>
    /// Create a new visit
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<VisitDto>> CreateVisit(VisitDto visitDto)
    {
        var visit = _mapper.Map<Visit>(visitDto);
        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, _mapper.Map<VisitDto>(visit));
    }

    /// <summary>
    /// Create a visit for a specific pet
    /// </summary>
    [HttpPost("{petId}/visits")]
    public async Task<ActionResult<VisitDto>> CreatePetVisit(int petId, VisitDto visitDto)
    {
        var pet = await _context.Pets.FindAsync(petId);
        if (pet == null)
        {
            return NotFound("Pet not found");
        }

        var visit = _mapper.Map<Visit>(visitDto);
        visit.PetId = petId;
        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, _mapper.Map<VisitDto>(visit));
    }

    /// <summary>
    /// Update a visit
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVisit(int id, VisitDto visitDto)
    {
        var visit = await _context.Visits.FindAsync(id);
        if (visit == null)
        {
            return NotFound();
        }

        _mapper.Map(visitDto, visit);
        visit.UpdatedAt = DateTime.UtcNow;

        _context.Visits.Update(visit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a visit
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVisit(int id)
    {
        var visit = await _context.Visits.FindAsync(id);
        if (visit == null)
        {
            return NotFound();
        }

        _context.Visits.Remove(visit);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
