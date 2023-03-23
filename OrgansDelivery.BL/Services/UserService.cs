using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Services;

namespace OrganStorage.BL.Services;

public interface IUserService
{
    Task<User> UpdateCurrentUserAsync(User user);
    Result<List<User>> GetUsersByTenantId(Guid tenantId);
}

public class UserService : IUserService
{
    private readonly IEnvironmentProvider _environmentProvider;
    private readonly AppDbContext _appDbContext;

    public UserService(
        IEnvironmentProvider environmentProvider,
        AppDbContext appDbContext
        )
    {
        _environmentProvider = environmentProvider;
        _appDbContext = appDbContext;
    }

    public Task<User> UpdateCurrentUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Result<List<User>> GetUsersByTenantId(Guid tenantId)
    {
        var exists = _appDbContext.Tenants.Any(t => t.Id == tenantId);
        if (!exists)
        {
            return Result.Fail("Tenant with given id does not exist");
        }

        return _appDbContext.Users.IgnoreQueryFilters().Where(u => u.TenantId == tenantId).ToList();
    }
}
