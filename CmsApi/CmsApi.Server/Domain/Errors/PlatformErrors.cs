namespace CmsApi.Server.Domain.Errors;

public static class PlatformErrors
{
    public static class Name
    {
        public const string Empty = "Platform name cannot be empty.";
        public const string InvalidLength = "Platform name must be between {0} and {1} characters.";
    }

    public static class General
    {
        public const string NotFound = "Platform with ID {0} not found.";
        public const string AlreadyExists = "The platform with code {0} already exists.";
    }
}
