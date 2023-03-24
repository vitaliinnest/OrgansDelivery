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
        // todo: CreateMap<UpdateTenantModel, Tenant>();

        // Invite
        CreateMap<InviteUserModel, Invite>();

        // Role
        CreateMap<IdentityRole<Guid>, RoleDto>();

        // ConditionPreset
        CreateMap<CreateConditionsPresetModel, Conditions>();
        // todo: CreateMap<UpdateConditionPresetModel, ConditionPreset>();

        // Container
        CreateMap<CreateContainerModel, Container>();
        // todo: CreateMap<UpdateContainerModel, Container>();

        // Organ
        CreateMap<CreateOrganModel, Organ>();

        // ContainerConditionsHistory
        CreateMap<CreateConditionsRecordModel, ConditionsRecord>();
    }
}
