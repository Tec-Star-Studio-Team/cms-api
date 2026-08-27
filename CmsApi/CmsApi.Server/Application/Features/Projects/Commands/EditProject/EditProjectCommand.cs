using CmsApi.Server.Application.Common.Models;
using Mediator;
using System.Text.Json.Serialization;

namespace CmsApi.Server.Application.Features.Projects.Commands.EditProject;

public sealed class EditProjectCommand : ICommand<Result<Unit>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
