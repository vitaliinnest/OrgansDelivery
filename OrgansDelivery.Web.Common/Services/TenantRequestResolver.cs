using Microsoft.AspNetCore.Http;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;
using OrgansDelivery.Web.Common.Extensions;

namespace OrgansDelivery.Web.Common.Services;

public interface ITenantRequestResolver
{
    Tenant ResolveTenant(HttpRequest request);
}

public class TenantRequestResolver : ITenantRequestResolver
{
    private readonly ITenantRepository _tenantProvider;
    private readonly IEnvironmentProvider _environmentProvider;

    public TenantRequestResolver(
        ITenantRepository tenantProvider,
        IEnvironmentProvider environmentProvider
        )
    {
        _tenantProvider = tenantProvider;
        _environmentProvider = environmentProvider;
    }

    public Tenant ResolveTenant(HttpRequest request)
    {
        var tenantUrl = request.Path.ExtractTenantUrl();

        if (!string.IsNullOrEmpty(tenantUrl))
        {
            return _tenantProvider.GetTenantByUrl(tenantUrl);
        }

        var user = _environmentProvider.User;
        if (user == null)
        {
            return null;
        }

        return _tenantProvider.GetTenantById(user.TenantId);
    }
}
