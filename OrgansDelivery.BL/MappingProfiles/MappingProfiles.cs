using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Models;
using OrganStorage.BL.Models.Auth;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.MappingProfiles;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        // Auth
        CreateMap<RegisterRequest, User>()
            .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Email));

        // User
        CreateMap<User, LoginResponse>();
        CreateMap<User, RegisterResponse>();
        CreateMap<User, UserDto>();

        // Tenant
        CreateMap<CreateTenantModel, Tenant>();
        
        // Invite
        CreateMap<InviteUserModel, Invite>();

        // Role
        CreateMap<IdentityRole<Guid>, RoleDto>();

        // ConditionPreset
        CreateMap<CreateContainerConditionsModel, Conditions>();
        
        // Container
        CreateMap<CreateContainerModel, Container>();
        
        // Organ
        CreateMap<CreateOrganModel, Organ>();
        CreateMap<UpdateOrganModel, Organ>()
            .ForMember(
                dest => dest.OrganCreationDate,
                opt => opt.MapFrom((src, dest) => src.OrganCreationDate ?? dest.OrganCreationDate))
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));

        // ContainerConditionsHistory
        CreateMap<CreateConditionsRecordModel, ConditionsRecord>();
    }
}
