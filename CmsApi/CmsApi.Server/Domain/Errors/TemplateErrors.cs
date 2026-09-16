namespace CmsApi.Server.Domain.Errors;

public static class TemplateErrors
{
    public static class Name
    {
        public const string Empty = "Template name cannot be empty.";
        public const string TooShort = "Template name must have at least {0} characters.";
        public const string TooLong = "Template name cannot exceed {0} characters.";
    }

    public static class Description
    {
        public const string Empty = "Template description cannot be empty.";
        public const string TooShort = "Template description must have at least {0} characters.";
        public const string TooLong = "Template description cannot exceed {0} characters.";
    }

    public static class General
    {
        public const string NotFound = "Template with ID {0} not found.";
        public const string AlreadyExists = "The template with ID {0} already exists.";
    }
}
