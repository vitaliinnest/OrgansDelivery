using AutoMapper;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.MappingProfiles;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<User, LoginResponse>();
        CreateMap<User, RegisterResponse>();
    }
}
