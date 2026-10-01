
using CmsApi.Server.Application.Common.Models;
using Mediator;

namespace CmsApi.Server.Application.Features.Templates.Commands.DeleteTemplate;

public sealed record DeleteTemplateCommand(int Id) : ICommand<Result<Unit>>;
