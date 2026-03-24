namespace PetClinicRest.DTOs;

public class VisitDto
{
    public int Id { get; set; }
    public DateTime VisitDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public int PetId { get; set; }
    public int VetId { get; set; }
    public VetDto? Vet { get; set; }
}
