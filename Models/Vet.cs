namespace PetClinicRest.Models;

public class Vet
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public ICollection<VetSpecialty> VetSpecialties { get; set; } = new List<VetSpecialty>();
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
