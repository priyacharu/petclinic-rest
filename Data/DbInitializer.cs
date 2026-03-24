using PetClinicRest.Models;

namespace PetClinicRest.Data;

public static class DbInitializer
{
    public static void Initialize(PetClinicDbContext context)
    {
        context.Database.EnsureCreated();

        // Check if database has been seeded
        if (context.Owners.Any() || context.PetTypes.Any())
        {
            return;
        }

        // Seed PetTypes
        var petTypes = new PetType[]
        {
            new PetType { Name = "Cat" },
            new PetType { Name = "Dog" },
            new PetType { Name = "Hamster" },
            new PetType { Name = "Rabbit" },
            new PetType { Name = "Bird" }
        };

        foreach (var petType in petTypes)
        {
            context.PetTypes.Add(petType);
        }
        context.SaveChanges();

        // Seed Specialties
        var specialties = new Specialty[]
        {
            new Specialty { Name = "Radiology" },
            new Specialty { Name = "Surgery" },
            new Specialty { Name = "Dentistry" }
        };

        foreach (var specialty in specialties)
        {
            context.Specialties.Add(specialty);
        }
        context.SaveChanges();

        // Seed Vets
        var vets = new Vet[]
        {
            new Vet { FirstName = "James", LastName = "Carter" },
            new Vet { FirstName = "Helen", LastName = "Leary" },
            new Vet { FirstName = "Linda", LastName = "Douglas" },
            new Vet { FirstName = "Rafael", LastName = "Ortega" },
            new Vet { FirstName = "Henry", LastName = "Stevens" }
        };

        foreach (var vet in vets)
        {
            context.Vets.Add(vet);
        }
        context.SaveChanges();

        // Seed VetSpecialties
        var vetSpecialties = new VetSpecialty[]
        {
            new VetSpecialty { VetId = 1, SpecialtyId = 1 }, // James Carter - Radiology
            new VetSpecialty { VetId = 3, SpecialtyId = 2 }, // Linda Douglas - Surgery
            new VetSpecialty { VetId = 3, SpecialtyId = 3 }, // Linda Douglas - Dentistry
            new VetSpecialty { VetId = 4, SpecialtyId = 2 }, // Rafael Ortega - Surgery
        };

        foreach (var vs in vetSpecialties)
        {
            context.VetSpecialties.Add(vs);
        }
        context.SaveChanges();

        // Seed Owners
        var owners = new Owner[]
        {
            new Owner { FirstName = "George", LastName = "Franklin", Address = "110 W. Liberty St.", City = "Madison", Telephone = "6085551023" },
            new Owner { FirstName = "Betty", LastName = "Davis", Address = "638 Cardinal Ave.", City = "Sun Prairie", Telephone = "6085551749" },
            new Owner { FirstName = "Eduardo", LastName = "Rodriquez", Address = "2693 Commerce St.", City = "McFarland", Telephone = "6085559435" },
            new Owner { FirstName = "Harold", LastName = "Davis", Address = "563 Friendly St.", City = "Windsor", Telephone = "6085553159" },
            new Owner { FirstName = "Peter", LastName = "McTavish", Address = "2387 S. Fair Way", City = "Madison", Telephone = "6085552765" },
            new Owner { FirstName = "Jean", LastName = "Coleman", Address = "105 N. Lake St.", City = "Monona", Telephone = "6085552654" },
            new Owner { FirstName = "Jeff", LastName = "Black", Address = "1450 Oak Blvd.", City = "Monona", Telephone = "6085555387" },
            new Owner { FirstName = "Maria", LastName = "Escobito", Address = "345 Maple St.", City = "Madison", Telephone = "6085551676" },
            new Owner { FirstName = "David", LastName = "Schroeder", Address = "2749 Blackhawk Trail", City = "Madison", Telephone = "6085559435" },
            new Owner { FirstName = "Carlos", LastName = "Estaban", Address = "2335 Independence La.", City = "Waunakee", Telephone = "6085555487" }
        };

        foreach (var owner in owners)
        {
            context.Owners.Add(owner);
        }
        context.SaveChanges();

        // Seed Pets
        var pets = new Pet[]
        {
            new Pet { Name = "Leo", BirthDate = new DateTime(2010, 9, 7), OwnerId = 1, PetTypeId = 1 }, // Cat
            new Pet { Name = "Basil", BirthDate = new DateTime(2012, 8, 6), OwnerId = 2, PetTypeId = 2 }, // Dog
            new Pet { Name = "Rosy", BirthDate = new DateTime(2011, 4, 17), OwnerId = 2, PetTypeId = 2 }, // Dog
            new Pet { Name = "Jewel", BirthDate = new DateTime(2010, 3, 7), OwnerId = 3, PetTypeId = 2 }, // Dog
            new Pet { Name = "Iggy", BirthDate = new DateTime(2010, 11, 30), OwnerId = 4, PetTypeId = 3 }, // Hamster
            new Pet { Name = "George", BirthDate = new DateTime(2010, 1, 20), OwnerId = 5, PetTypeId = 4 }, // Rabbit
            new Pet { Name = "Samantha", BirthDate = new DateTime(2012, 9, 4), OwnerId = 6, PetTypeId = 1 }, // Cat
            new Pet { Name = "Max", BirthDate = new DateTime(2012, 9, 4), OwnerId = 6, PetTypeId = 1 }, // Cat
            new Pet { Name = "Lucky", BirthDate = new DateTime(2011, 8, 6), OwnerId = 7, PetTypeId = 2 }, // Dog
            new Pet { Name = "Mulligan", BirthDate = new DateTime(2007, 2, 24), OwnerId = 8, PetTypeId = 2 } // Dog
        };

        foreach (var pet in pets)
        {
            context.Pets.Add(pet);
        }
        context.SaveChanges();

        // Seed Visits
        var visits = new Visit[]
        {
            new Visit { VisitDate = new DateTime(2024, 1, 1), Description = "rabies shot", PetId = 7, VetId = 1 },
            new Visit { VisitDate = new DateTime(2024, 3, 4), Description = "distemper shot", PetId = 8, VetId = 1 },
            new Visit { VisitDate = new DateTime(2024, 3, 4), Description = "Routine checkup", PetId = 2, VetId = 1 }
        };

        foreach (var visit in visits)
        {
            context.Visits.Add(visit);
        }
        context.SaveChanges();
    }
}
