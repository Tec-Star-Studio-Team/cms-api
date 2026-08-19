# Creates the initial migration with both schemas
`dotnet ef migrations add InitialCreate --project CmsApi.Server --output-dir Infrastructure/Persistence/Migrations`

# Applies the migration
`dotnet ef database update --project CmsApi.Server`

# NBomber for the Load Tests
`dotnet add package NBomber`

`dotnet add package NBomber.Http`

`dotnet add package NBomber.Data`

`dotnet add package Bogus`

```
var faker = new Faker<ProductRequest>("pt_BR")
    .RuleFor(p => p.Name,  f => f.Commerce.ProductName())
    .RuleFor(p => p.Price, f => f.Random.Decimal(10, 5000))
    .RuleFor(p => p.Stock, f => f.Random.Int(1, 200));

var products = faker.Generate(1000);
```