namespace CmsApi.Server.Domain.Errors;

public static class LanguageErrors
{
    public static class Code
    {
        public const string Empty = "Language code cannot be empty.";
        public const string InvalidLength = "Language code must be between {0} and {1} characters.";
        public const string IsNotValid = "The language code {0} is not valid.";
    }

    public static class Name
    {
        public const string Empty = "Language code cannot be empty.";
        public const string InvalidLength = "Language name must be between {0} and {1} characters.";
    }

    public static class General
    {
        public const string InvalidId = "The language ID is invalid.";
        public const string NotFound = "Language with ID {0} not found.";
        public const string AlreadyExists = "The language with code {0} already exists.";
    }
}
