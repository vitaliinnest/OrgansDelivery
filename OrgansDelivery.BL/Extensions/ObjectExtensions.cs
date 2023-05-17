using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace OrganStorage.BL.Extensions;

public static class ObjectExtensions
{
	public static TObject LogObject<TObject, TEntity>(this TObject @object, ILogger<TEntity> logger, string msg = null)
		where TEntity : class
	{
		if (msg != null)
		{
			logger.LogInformation(msg);
		}
		var output = "NULL";
		if (@object != null)
		{
			output = JsonSerializer.Serialize(@object, new JsonSerializerOptions
			{
				WriteIndented = true
			});
		}

		logger.LogInformation($"[{@object?.GetType().Name}]:\r\n{output}");
		return @object;
	}
}
