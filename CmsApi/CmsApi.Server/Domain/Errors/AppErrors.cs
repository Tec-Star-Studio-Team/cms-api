namespace CmsApi.Server.Domain.Errors;

public static class AppErrors
{
    public static class Name
    {
        public const string Empty = "App name cannot be empty.";
        public const string TooShort = "App name must have at least {0} characters.";
        public const string TooLong = "App name cannot exceed {0} characters.";
    }

    public static class General
    {
        public const string NotFound = "App with ID {0} not found.";
        public const string AlreadyExists = "The app with code {0} already exists.";
    }
}
