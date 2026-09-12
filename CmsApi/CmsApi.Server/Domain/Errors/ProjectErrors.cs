namespace CmsApi.Server.Domain.Errors;

public static class ProjectErrors
{
    public static class Name
    {
        public const string Empty = "Project name cannot be empty.";
        public const string TooShort = "Project name must have at least {0} characters.";
        public const string TooLong = "Project name cannot exceed {0} characters.";
    }

    public static class Description
    {
        public const string TooLong = "Project description cannot exceed {0} characters.";
    }

    public static class General
    {
        public const string NotFound = "Project with ID {0} not found.";
        public const string AlreadyExists = "The project with name {0} already exists.";
    }
}
