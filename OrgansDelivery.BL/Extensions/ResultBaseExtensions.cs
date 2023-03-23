using FluentResults;

namespace OrganStorage.BL.Extensions;

public static class ResultBaseExtensions
{
    public static string ErrorMessagesToString(this ResultBase resultBase)
    {
        return string.Join("; ", resultBase.Errors.Select(e => e.Message));
    }
}
