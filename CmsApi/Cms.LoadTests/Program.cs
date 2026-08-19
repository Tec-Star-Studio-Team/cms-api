using Bogus;
using CmsApi.Server.Domain.Entities;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Data;
using NBomber.Data.CSharp;
using System.Text;
using System.Text.Json;

var httpClient = new HttpClient();

var projectEndpointScenario = GetInsertProjectsSimulation(httpClient);

NBomberRunner
    .RegisterScenarios(projectEndpointScenario)
    .Run();

// Generate a project insertion scenario
static ScenarioProps GetInsertProjectsSimulation(HttpClient httpClient)
{
    var faker = new Faker<Project>("en")
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());

    var projects = faker.Generate(200);
    IDataFeed<Project> feed = DataFeed.Random(projects.AsEnumerable());

    var scenario = Scenario.Create("insert_projects", async context =>
    {
        var product = feed.GetNextItem(context.ScenarioInfo);

        var json = JsonSerializer.Serialize(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("http://localhost:5480/api/projects", content);

        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
    })
    .WithLoadSimulations(
        Simulation.RampingInject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)), // sobe até 50 req/s
        Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(60)), // mantém 50 req/s
        Simulation.RampingInject(rate: 0, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15))  // desce até 0
    );

    return scenario;
}