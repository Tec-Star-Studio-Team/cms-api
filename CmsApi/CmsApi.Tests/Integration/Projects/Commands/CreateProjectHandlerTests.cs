using CmsApi.Tests.Common;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace CmsApi.Tests.Integration.Projects.Commands;

[Trait("Category", "Integration")]
[Collection(nameof(SqlServerCollection))]
public sealed class CreateProjectHandlerTests : IAsyncLifetime
{
    private readonly CmsApiFactory _factory;
    private HttpClient _client = null!;
    private string _token = string.Empty;

    public CreateProjectHandlerTests(SqlServerContainerFixture fixture)
    {
        // Factory receives the already-running container connection string
        _factory = new CmsApiFactory(fixture.ConnectionString);
    }

    public async Task InitializeAsync()
    {
        _client = _factory.CreateClient();
        _token = await AuthHelper.GetTokenAsync(_client);
    }

    public async Task DisposeAsync()
    {
        await _factory.ResetDatabaseAsync();
        await _factory.DisposeAsync();
    }

    [Fact]
    public async Task CreateProject_WhenValidRequest_ShouldReturn201()
    {
        // Arrange
        var request = new { name = "My CMS Project", description = "Test description" };

        // Act
        var response = await _client
            .WithBearerToken(_token)
            .PostAsJsonAsync("api/projects", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
