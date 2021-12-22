set ASPNETCORE_ENVIRONMENT=Local

Be at the root level then execute those commands to create the migration :

dotnet ef migrations --project=POC.Infrastructure --startup-project=POC.API add initialMigration -c PaymentContext

initial migration :
dotnet ef migrations script --project=POC.Infrastructure --startup-project=POC.API -c PaymentContext -o ./POC.Infrastructure/DB/Schema/V1.1__inital_migration.sql -v

new migration :
dotnet ef migrations script --project=POC.Infrastructure --startup-project=POC.API -c PaymentContext initialMigration -o ./POC.Infrastructure/DB/Schema/V1.1__inital_migration.sql -v