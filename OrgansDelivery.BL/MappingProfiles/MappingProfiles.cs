using AutoMapper;
using OrgansDelivery.BL.Models;
using OrgansDelivery.BL.Models.Auth;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.MappingProfiles;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        // Auth
        CreateMap<RegisterRequest, User>()
            .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Email));

        CreateMap<User, LoginResponse>();
        CreateMap<User, RegisterResponse>();
        CreateMap<UpdateUserModel, User>();

        // Tenant
        CreateMap<CreateTenantModel, Tenant>();
        CreateMap<UpdateTenantModel, Tenant>();

        // Invite
        CreateMap<InviteUserModel, Invite>();
    }
}
