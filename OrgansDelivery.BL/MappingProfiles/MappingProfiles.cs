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

        CreateMap<User, LoginResponse>();
        CreateMap<User, RegisterResponse>();
        // todo: CreateMap<UpdateUserModel, User>();

        // Tenants
        CreateMap<CreateTenantModel, Tenant>();
        // todo: CreateMap<UpdateTenantModel, Tenant>();

        // Invites
        CreateMap<InviteUserModel, Invite>();

        // Roles
        CreateMap<IdentityRole<Guid>, RoleDto>();

        // ConditionPresets
        CreateMap<CreateConditionsPresetModel, ConditionPreset>();
        // todo: CreateMap<UpdateConditionPresetModel, ConditionPreset>();

        // Containers
        CreateMap<CreateContainerModel, Container>();
        // todo: CreateMap<UpdateContainerModel, Container>();

        // Organs
        CreateMap<CreateOrganModel, Organ>();

        // ContainerConditionsHistory
        CreateMap<CreateConditionsRecordModel, ContainerConditionsRecord>();
    }
}
