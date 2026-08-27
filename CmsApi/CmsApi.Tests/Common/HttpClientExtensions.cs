using System.Net.Http.Headers;

namespace CmsApi.Tests.Common;

public static class HttpClientExtensions
{
    public static HttpClient WithBearerToken(this HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
