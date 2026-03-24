namespace PetClinicRest.Models;

public class Visit
{
    public int Id { get; set; }
    public DateTime VisitDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public int PetId { get; set; }
    public int VetId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Pet Pet { get; set; } = null!;
    public Vet Vet { get; set; } = null!;
}
