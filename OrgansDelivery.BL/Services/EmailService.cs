using Microsoft.Extensions.Options;
using OrganStorage.BL.Models.Options;
using OrganStorage.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace OrganStorage.BL.Services;

public interface IEmailService
{
    Task SendEmailConfirmationMailAsync(User user, string emailConfirmationToken);
    Task SendInviteMailMessageAsync(Invite invite);
}

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly IEmailMessageBuilder _emailMessageBuilder;

    public EmailService(
        IOptions<SmtpSettings> smtpSettingOpts,
        IEmailMessageBuilder emailMessageBuilder
        )
    {
        var smtpSetting = smtpSettingOpts.Value;
        _smtpClient = new(smtpSetting.Host, smtpSetting.Port)
        {
            Credentials = new NetworkCredential(smtpSetting.User, smtpSetting.Password)
        };
        _emailMessageBuilder = emailMessageBuilder;
    }

    public async Task SendEmailConfirmationMailAsync(User user, string emailConfirmationToken)
    {
        var emailConfirmationMessage = _emailMessageBuilder
            .BuildEmailConfirmationMessage(user, emailConfirmationToken);
        await _smtpClient.SendMailAsync(emailConfirmationMessage);
    }

    public async Task SendInviteMailMessageAsync(Invite invite)
    {
        var inviteMessage = _emailMessageBuilder.BuildInviteMessage(invite);
        await _smtpClient.SendMailAsync(inviteMessage);
    }
}
