using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;

namespace OrganStorage.BL.Services;

public interface IUserService
{
    Task<Result<UserDto>> GetCurrentUserAsync();
    Task<Result<UserDto>> UpdateUser(UserFormValues model);
    Result<List<User>> GetUsersByTenantId(Guid tenantId);
}

public class UserService : IUserService
{
    private readonly IEnvironmentProvider _environmentProvider;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;

    public UserService(
        IEnvironmentProvider environmentProvider,
        AppDbContext context,
        IMapper mapper,
        IRoleService roleService)
    {
        _environmentProvider = environmentProvider;
        _context = context;
        _mapper = mapper;
        _roleService = roleService;
    }

    public async Task<Result<UserDto>> GetCurrentUserAsync()
    {
        var user = _environmentProvider.User;
        if (user == null)
        {
            return Result.Fail("User not set");
        }

        return await UserToDtoAsync(user);
    }

    public async Task<Result<UserDto>> UpdateUser(UserFormValues model)
    {
        // imagine validation here

        var user = _environmentProvider.User;
        if (user == null)
        {
            return Result.Fail("User not set");
        }

        var updated = _mapper.Map(model, user);

        _context.Update(updated);
        _context.SaveChanges();

        var dto = await UserToDtoAsync(updated);

        return dto;
    }

    public Result<List<User>> GetUsersByTenantId(Guid tenantId)
    {
        var exists = _context.Tenants.Any(t => t.Id == tenantId);
        if (!exists)
        {
            return Result.Fail("Tenant with given id does not exist");
        }

        return _context.Users.IgnoreQueryFilters().Where(u => u.TenantId == tenantId).ToList();
    }

    private async Task<UserDto> UserToDtoAsync(User user)
    {
        var dto = _mapper.Map<UserDto>(user);
        var userRole = await _roleService.GetUserRoleAsync(user);
        dto.Tenant = _environmentProvider.Tenant;
        dto.Role = userRole != null
            ? _mapper.Map<RoleDto>(userRole)
            : null;
        return dto;
    }
}
