namespace PetClinicRest.Models;

public class VetSpecialty
{
    public int VetId { get; set; }
    public int SpecialtyId { get; set; }

    public Vet Vet { get; set; } = null!;
    public Specialty Specialty { get; set; } = null!;
}
