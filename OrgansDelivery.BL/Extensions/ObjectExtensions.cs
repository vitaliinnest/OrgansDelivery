﻿using System.Text.Json;

namespace OrganStorage.BL.Extensions;

public static class ObjectExtensions
{
	public static TObject DumpToConsole<TObject>(this TObject @object)
	{
		var output = "NULL";
		if (@object != null)
		{
			output = JsonSerializer.Serialize(@object, new JsonSerializerOptions
			{
				WriteIndented = true
			});
		}

		Console.WriteLine($"[{@object?.GetType().Name}]:\r\n{output}");
		return @object;
	}
}