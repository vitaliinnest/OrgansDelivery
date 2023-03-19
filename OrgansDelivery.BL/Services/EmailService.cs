using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrgansDelivery.BL.Models.Options;
using OrgansDelivery.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace OrgansDelivery.BL.Services;

public interface IEmailService
{
    Task SendEmailConfirmationMailAsync(User user);
    Task SendInviteMailMessageAsync(Invite invite);
}

public class EmailService : IEmailService
{
    private readonly SmtpClient _emailClient;
    private readonly EmailMessageBuilder _emailMessageBuilder;

    public EmailService(
        IOptions<SmtpSettings> smtpSettingOpts,
        EmailMessageBuilder emailMessageBuilder
        )
    {
        var smtpSetting = smtpSettingOpts.Value;
        _emailClient = new(smtpSetting.Host, smtpSetting.Port)
        {
            Credentials = new NetworkCredential(smtpSetting.User, smtpSetting.Password)
        };
        _emailMessageBuilder = emailMessageBuilder;
    }

    public async Task SendEmailConfirmationMailAsync(User user)
    {
        var message = await _emailMessageBuilder.BuildEmailConfirmationMessageAsync(user);
        await _emailClient.SendMailAsync(message);
    }

    public async Task SendInviteMailMessageAsync(Invite invite)
    {
        var message = await _emailMessageBuilder.BuildInviteMessageAsync(invite);
        await _emailClient.SendMailAsync(message);
    }
}
