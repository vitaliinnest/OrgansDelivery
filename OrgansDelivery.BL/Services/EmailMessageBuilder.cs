using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.BL.Models.Options;
using OrgansDelivery.BL.Consts;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Enums;
using System.Net.Mail;
using System.Web;

namespace OrgansDelivery.BL.Services;

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
        
        return user.Language switch
        {
            Language.Ukrainian => new MailMessage(
                from: _smtpSetting.Sender,
                to: user.Email,
                subject: $"{MetadataConsts.COMPANY_NAME}: підтвердіть вашу електронну адресу",
                body: $"Перейдіть за цим посиланням для підтвердження вашої електронної адреси:\n" +
                    $"{confirmationLink}"),
            Language.English or _ => new MailMessage(
                from: _smtpSetting.Sender,
                to: user.Email,
                subject: $"{MetadataConsts.COMPANY_NAME}: confirm your email",
                body: $"Please click on this link to confirm your email address:\n" +
                    $"{confirmationLink}"),
        };
    }

    public MailMessage BuildInviteMessage(Invite invite)
    {
        return invite.Language switch
        {
            Language.Ukrainian => new MailMessage(
                from: _smtpSetting.Sender,
                to: invite.Email,
                subject: $"{MetadataConsts.COMPANY_NAME}: запрошення",
                body: $"Код запрошення: <b>{invite.InviteCode}</b>"),
            Language.English or _ => new MailMessage(
                from: _smtpSetting.Sender,
                to: invite.Email,
                subject: $"{MetadataConsts.COMPANY_NAME}: invitation",
                body: $"Invite code: <b>{invite.InviteCode}</b>"),
        };
    }

    private string BuildEmailConfirmationLink(User user, string emailConfirmationToken)
    {
        var encodedToken = HttpUtility.UrlEncode(emailConfirmationToken.ToBase64Encoded());
        return $"{_configuration["EmailConfirmationUrl"]}?userId={user.Id}&token={encodedToken}";
    }
}
