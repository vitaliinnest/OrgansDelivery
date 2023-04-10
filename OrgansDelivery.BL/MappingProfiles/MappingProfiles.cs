using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Models;
using OrganStorage.BL.Models.Auth;
using OrganStorage.DAL.Entities;
using System.Globalization;

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
        CreateMap<UpdateUserModel, User>()
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));

        // Tenant
        CreateMap<CreateTenantModel, Tenant>();
        CreateMap<UpdateTenantModel, Tenant>()
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));

        // Invite
        CreateMap<InviteUserModel, Invite>();

        // Role
        CreateMap<IdentityRole<Guid>, RoleDto>();

        // ConditionPreset
        CreateMap<CreateContainerConditionsModel, Conditions>();
        CreateMap<UpdateConditionsModel, Conditions>()
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));

        // Container
        CreateMap<CreateContainerModel, Container>();
        CreateMap<UpdateContainerModel, Container>()
            .ForMember(
                dest => dest.ConditionsId,
                opt => opt.MapFrom((src, dest) => src.ConditionsId ?? dest.ConditionsId))
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));

        // Organ
        CreateMap<CreateOrganModel, Organ>();
        CreateMap<UpdateOrganModel, Organ>()
            .ForMember(
                dest => dest.OrganCreationDate,
                opt => opt.MapFrom((src, dest) => src.OrganCreationDate ?? dest.OrganCreationDate))
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));

        // ContainerConditionsHistory
        CreateMap<CreateConditionsRecordModel, ConditionsRecord>()
            .ForMember(
                dest => dest.Orientation,
                opt => opt.MapFrom(src => new Orientation() { X = src.Ort_x, Y = src.Ort_y }))
            .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.Sent_at_utc));
        CreateMap<ConditionsRecord, ConditionsRecordDto>();
    }
}
