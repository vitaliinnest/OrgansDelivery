namespace OrgansDelivery.Web.Extensions;

public static class ILoggerExtensions
{
    public static bool LogException(this ILogger logger, Exception exception)
    {
        logger.LogError(exception, message: null);
        return false;
    }
}
