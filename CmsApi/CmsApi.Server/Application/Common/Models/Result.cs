namespace CmsApi.Server.Application.Common.Models;

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsNotFound { get; }
    public bool IsFailure => !IsSuccess && !IsNotFound;
    public T? Value { get; }
    public string? Error { get; }

    private Result(T value) { IsSuccess = true; Value = value; }
    private Result(string? error, bool notFound)
    {
        if (notFound)
        {
            IsNotFound = true;
            Error = error;
        }
        else Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T?> Success() => new(default(T));
    public static Result<T> Failure(string error) => new(error, notFound: false);
    public static Result<T> NotFound(string error) => new(error, notFound: true);
}
