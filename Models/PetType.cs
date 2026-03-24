namespace PetClinicRest.Models;

public class PetType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
