using CmsApi.Server.Domain.Entities;

namespace CmsApi.Domain.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
