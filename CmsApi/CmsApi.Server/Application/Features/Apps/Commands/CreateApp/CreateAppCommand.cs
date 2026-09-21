using CmsApi.Server.Application.Common.Models;
using Mediator;

namespace CmsApi.Server.Application.Features.Apps.Commands.CreateApp;

public sealed record CreateAppCommand(string Name, int ProjectId, int LanguageId, int TemplateId) : ICommand<Result<Unit>>;