namespace PetClinicRest.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int OwnerId { get; set; }
    public int PetTypeId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Owner Owner { get; set; } = null!;
    public PetType PetType { get; set; } = null!;
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
