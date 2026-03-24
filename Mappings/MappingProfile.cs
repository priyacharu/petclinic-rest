using AutoMapper;
using PetClinicRest.Models;
using PetClinicRest.DTOs;

namespace PetClinicRest.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Owner, OwnerDto>().ReverseMap();
        CreateMap<Pet, PetDto>().ReverseMap();
        CreateMap<Vet, VetDto>()
            .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.VetSpecialties.Select(vs => vs.Specialty)))
            .ReverseMap();
        CreateMap<Specialty, SpecialtyDto>().ReverseMap();
        CreateMap<PetType, PetTypeDto>().ReverseMap();
        CreateMap<Visit, VisitDto>().ReverseMap();
    }
}
