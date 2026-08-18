using Microsoft.AspNetCore.Identity;

namespace CmsApi.Server.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public string FullName => $"{FirstName} {LastName}".Trim();
}
