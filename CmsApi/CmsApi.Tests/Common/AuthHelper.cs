using System.Net.Http.Json;

namespace CmsApi.Tests.Common;

public static class AuthHelper
{
    public static async Task<string> GetTokenAsync(HttpClient client)
    {
        // Register a test user
        await client.PostAsJsonAsync("api/auth/register", new
        {
            firstName = "Test",
            lastName = "User",
            email = "test@cmsapi.com",
            password = "Test@1234!"
        });

        // Login and get the JWT token
        var response = await client.PostAsJsonAsync("api/auth/login", new
        {
            email = "test@cmsapi.com",
            password = "Test@1234!"
        });

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        return result!.Token;
    }

    private sealed record AuthResponse(string Token);
}
