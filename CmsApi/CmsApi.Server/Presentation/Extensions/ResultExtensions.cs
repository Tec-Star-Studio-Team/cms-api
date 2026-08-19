using CmsApi.Server.Application.Common.Models;

namespace CmsApi.Server.Presentation.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(this Result<T> result)
        => result switch
        {
            { IsSuccess: true } => Results.Ok(result.Value),
            { IsNotFound: true } => Results.NotFound(new { error = result.Error }),
            _ => Results.BadRequest(new { error = result.Error })
        };

    public static IResult ToHttpResult<T>(this Result<T> result, string createdAtRoute)
        => result switch
        {
            { IsSuccess: true } => Results.Created(createdAtRoute, result.Value),
            { IsNotFound: true } => Results.NotFound(new { error = result.Error }),
            _ => Results.BadRequest(new { error = result.Error })
        };
}
