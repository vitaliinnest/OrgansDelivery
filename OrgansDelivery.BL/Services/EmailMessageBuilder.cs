using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Entities;
using System.Net.Mail;

namespace OrgansDelivery.BL.Services;

public interface IEmailMessageBuilder
{
    Task<MailMessage> BuildEmailConfirmationMessageAsync(User user);
    Task<MailMessage> BuildInviteMessageAsync(Invite invite);
}

public class EmailMessageBuilder : IEmailMessageBuilder
{
    private readonly UserManager<User> _userManager;

    public EmailMessageBuilder(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Task<MailMessage> BuildEmailConfirmationMessageAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<MailMessage> BuildInviteMessageAsync(Invite invite)
    {
        throw new NotImplementedException();
    }
}
