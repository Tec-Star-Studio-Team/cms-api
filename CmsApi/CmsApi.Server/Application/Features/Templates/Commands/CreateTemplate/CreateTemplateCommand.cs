using CmsApi.Server.Application.Common.Models;
using Mediator;

namespace CmsApi.Server.Application.Features.Templates.Commands.CreateTemplate;

public sealed record CreateTemplateCommand(string Name, string Description) : ICommand<Result<Unit>>;
