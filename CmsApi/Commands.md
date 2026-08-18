# Creates the initial migration with both schemas
`dotnet ef migrations add InitialCreate --project CmsApi.Server --output-dir Infrastructure/Persistence/Migrations`

# Applies the migration
`dotnet ef database update --project CmsApi.Server`