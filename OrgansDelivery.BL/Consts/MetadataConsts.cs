using System.Text.RegularExpressions;

namespace OrgansDelivery.BL.Consts;

public static class MetadataConsts
{
    public const string COMPANY_NAME = "OrgansDelivery";
}

public static class ValidatorConsts
{
    public static class UserConsts
    {
        public const int NAME_MIN_LENGTH = 2;
        public const int NAME_MAX_LENGTH = 50;
        public static readonly Regex NAME_REGEX = new(@"^[^\W\d_]+(?:-[^\W\d_]+)*\.?$");
    }
}
