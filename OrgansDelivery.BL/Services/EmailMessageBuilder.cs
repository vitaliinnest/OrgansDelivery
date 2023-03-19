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
    public Task<MailMessage> BuildEmailConfirmationMessageAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<MailMessage> BuildInviteMessageAsync(Invite invite)
    {
        throw new NotImplementedException();
    }
}
