namespace CmsApi.Server.Domain.Errors;

public static class TemplateErrors
{
    public static class Name
    {
        public const string Empty = "Template name cannot be empty.";
        public const string InvalidLength = "Template name must be between {0} and {1} characters.";
    }

    public static class Description
    {
        public const string Empty = "Template description cannot be empty.";
        public const string InvalidLength = "Template description must be between {0} and {1} characters.";
    }

    public static class General
    {
        public const string InvalidId = "The template ID is invalid.";
        public const string NotFound = "Template with ID {0} not found.";
        public const string AlreadyExists = "The template with ID {0} already exists.";
    }
}
