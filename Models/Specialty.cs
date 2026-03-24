namespace PetClinicRest.Models;

public class Specialty
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<VetSpecialty> VetSpecialties { get; set; } = new List<VetSpecialty>();
}
