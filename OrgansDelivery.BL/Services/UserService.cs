using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;

namespace OrgansDelivery.BL.Services;

public interface IUserService
{
    User CurrentUser { get; }
    Task<User> UpdateUserAsync(User user);
}

public class UserService : IUserService
{
    public UserService(
        IEnvironmentProvider environmentProvider
        )
    {
        CurrentUser = environmentProvider.User;
    }

    public User CurrentUser { get; }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
