using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OrganStorage.DAL.Services;

public static class ValueConverterBuilder
{
    public static ValueConverter<DateTime, DateTime> BuildDateTimeConverter()
    {
        return new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }

    public static ValueConverter<DateTime?, DateTime?> BuildNullableDateTimeConverter()
    {
        return new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);
    }
}
