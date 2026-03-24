namespace PetClinicRest.DTOs;

public class PetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int OwnerId { get; set; }
    public int PetTypeId { get; set; }
    public PetTypeDto? PetType { get; set; }
    public List<VisitDto> Visits { get; set; } = new List<VisitDto>();
}
