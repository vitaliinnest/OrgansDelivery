using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Web;
using OrganStorage.BL.Extensions;
using OrganStorage.BL.Consts;

using OrganStorage.DAL.Entities;
using OrganStorage.BL.Models.Options;
using OrganStorage.DAL.Services;

namespace OrganStorage.BL.Services;

public interface IEmailMessageBuilder
{
    MailMessage BuildEmailConfirmationMessage(User user, string emailConfirmationToken);
    MailMessage BuildInviteMessage(Invite invite);
}

public class EmailMessageBuilder : IEmailMessageBuilder
{
    private readonly IConfiguration _configuration;
    private readonly SmtpSettings _smtpSetting;

    public EmailMessageBuilder(
        IConfiguration configuration,
        IOptions<SmtpSettings> smtpSetting
		)
    {
        _configuration = configuration;
        _smtpSetting = smtpSetting.Value;
	}

    public MailMessage BuildEmailConfirmationMessage(User user, string emailConfirmationToken)
    {
        var confirmationLink = BuildEmailConfirmationLink(user, emailConfirmationToken);
        return new MailMessage(
                from: _smtpSetting.Sender,
                to: user.Email,
                subject: $"{MetadataConsts.COMPANY_NAME}: confirm your email",
                body: $"Please click on this link to confirm your email address:\n" +
                    $"{confirmationLink}");
    }

    public MailMessage BuildInviteMessage(Invite invite)
    {
        return new MailMessage(
                from: _smtpSetting.Sender,
                to: invite.Email,
                subject: $"{MetadataConsts.COMPANY_NAME}: invitation",
                body: $"Invite code: <b>{invite.InviteCode}</b>");

	}

    private string BuildEmailConfirmationLink(User user, string emailConfirmationToken)
    {
        var encodedToken = HttpUtility.UrlEncode(emailConfirmationToken.ToBase64Encoded());
        return $"{_configuration["EmailConfirmationUrl"]}?userId={user.Id}&token={encodedToken}";
    }
}
