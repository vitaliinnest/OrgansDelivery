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

        CreateMap<UpdateUserModel, User>()
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));


        // Tenant
        CreateMap<CreateTenantModel, Tenant>();

        CreateMap<UpdateTenantModel, Tenant>()
            .ForAllMembers(o => o.Condition((src, dest, value) => value != null));


        // Invite
        CreateMap<InviteFormValues, Invite>();


        // Role
        CreateMap<IdentityRole<Guid>, RoleDto>();


        // Conditions
        CreateMap<Conditions, Conditions>();
        CreateMap<Conditions, ConditionsDto>();
        CreateMap<ConditionsFormValues, Conditions>();

        // Container
        CreateMap<ContainerFormValues, Container>();


        // Organ
        CreateMap<Organ, OrganDto>();
        CreateMap<OrganFormValues, Organ>();


        // ConditionsRecord
        CreateMap<CreateConditionsRecordModel, ConditionsRecord>()
            .ForMember(
                dest => dest.Orientation,
                opt => opt.MapFrom(src => new Orientation() { X = src.Ort_x, Y = src.Ort_y }))
            .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.Sent_at_utc));
        
        CreateMap<ConditionsRecord, ConditionsRecordDto>();


        // Device
        CreateMap<DeviceFormValues, Device>();
		CreateMap<Device, DeviceConfigurationMessage>()
			.ForMember(dest => dest.Interval_ms, opt => opt.MapFrom(src => src.ConditionsIntervalCheckInMs));
	}
}
