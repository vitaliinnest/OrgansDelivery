using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;

namespace OrgansDelivery.BL.Services;

public interface IUserService
{
    Task<User> UpdateCurrentUserAsync(User user);
}

public class UserService : IUserService
{
    private readonly IEnvironmentProvider _environmentProvider;

    public UserService(
        IEnvironmentProvider environmentProvider
        )
    {
        _environmentProvider = environmentProvider;
    }

    public Task<User> UpdateCurrentUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
