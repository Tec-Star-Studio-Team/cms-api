namespace CmsApi.Server.Domain.Errors;

public static class ProjectErrors
{
    public static class Name
    {
        public const string Empty = "Project name cannot be empty.";
        public const string InvalidLength = "Project name must be between {0} and {1} characters.";
    }

    public static class Description
    {
        public const string InvalidLength = "Project description max length is {0} characters.";
    }

    public static class General
    {
        public const string InvalidId = "The project ID is invalid.";
        public const string NotFound = "Project with ID {0} not found.";
        public const string AlreadyExists = "The project with name {0} already exists.";
    }
}
