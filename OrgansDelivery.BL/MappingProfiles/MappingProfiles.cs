﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrganStorage.BL.Models;
using OrganStorage.BL.Models.Auth;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
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
        CreateMap<TenantFormValues, Tenant>();

        // Invite
        CreateMap<InviteFormValues, Invite>();

        // Role
        CreateMap<IdentityRole<Guid>, RoleDto>();

        // Conditions
        CreateMap<Conditions, Conditions>();
        CreateMap<Conditions, ConditionsDto>();
        CreateMap<ConditionsFormValues, Conditions>();

        // Container
        CreateMap<Container, ContainerDto>();
        CreateMap<ContainerFormValues, Container>();

        // Organ
        CreateMap<Organ, OrganDto>()
            .ForMember(
                dest => dest.Conditions,
                opt => opt.MapFrom(src => src != null
                    ? new ConditionsRef
                    {
                        Id = src.Conditions.Id,
                        Name = src.Conditions.Name,
                        Description = src.Conditions.Description,
                        Humidity = src.Conditions.Humidity,
                        Light = src.Conditions.Light,
                        Orientation = src.Conditions.Orientation,
                        Temperature = src.Conditions.Temperature,
                    }
                    : null))
            .ForMember(
                dest => dest.Container,
                opt => opt.MapFrom(src => src != null
                    ? new ContainerRef
                    {
                        Id = src.Container.Id,
                        Name = src.Container.Name,
                        Description = src.Container.Description,
                        DeviceId = src.Container.DeviceId,
                        OrganId = src.Container.Organ.Id
                    }
                    : null));
        //CreateMap<Organ, OrganDto>();
			//.ForPath(
			//	dest => dest.Container.OrganId,
			//	opt => opt.MapFrom(src => src.Container.Organ.Id));
		CreateMap<OrganFormValues, Organ>();

        // ConditionsRecord
        CreateMap<CreateConditionsRecordModel, ConditionsRecord>()
            .ForMember(
                dest => dest.Orientation,
                opt => opt.MapFrom(src => new Orientation() { X = src.Ort_x, Y = src.Ort_y }))
            .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.Sent_at_utc));        
        CreateMap<ConditionsRecord, ConditionsRecordDto>();


        // Device
        CreateMap<Device, DeviceDto>();
        CreateMap<DeviceFormValues, Device>();
		CreateMap<Device, DeviceConfigurationMessage>()
			.ForMember(dest => dest.Interval_ms, opt => opt.MapFrom(src => src.ConditionsIntervalCheckInMs));
	}
}
