using System.Text.RegularExpressions;

namespace OrganStorage.BL.Consts;

public static class MetadataConsts
{
    public const string COMPANY_NAME = "OrganStorage";
}

public static class ValidatorConsts
{
    public static class UserConsts
    {
        public const int NAME_MIN_LENGTH = 2;
        public const int NAME_MAX_LENGTH = 50;
        public static readonly Regex NAME_REGEX = new(@"^[^\W\d_]+(?:-[^\W\d_]+)*\.?$");
    }

    public static class GeneralConsts
    {
        public const int MIN_LENGTH = 3;
        public const int MAX_LENGTH = 300;
        public static readonly Regex STR_INT_DASH_REGEX = new(@"^[A-Za-z0-9-]*$");
    }
}

public static class ConditionConsts
{
    public static class Temperature
    {
        public const decimal MIN = -100m;
        public const decimal MAX = 100m;
    }

    public static class Humidity
    {
        public const decimal MIN = 0m;
        public const decimal MAX = 100m;
    }

    public static class Light
    {
        public const decimal MIN = 0m;
        public const decimal MAX = 20000m;
    }

    public static class OrientationAxis
    {
        public const decimal MIN = -90m;
        public const decimal MAX = 90m;
    }
}
