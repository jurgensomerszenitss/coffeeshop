SQL Migrations :
This command adds a new migrations
	dotnet ef migrations add <name> -o Migrations

Create/update the database
	dotnet ef database update


Setup local redis :
docker run -p 6379:6379 -d --name redis-local redis 