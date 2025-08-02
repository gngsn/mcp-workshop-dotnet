using McpTodoServer.ContainerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Create and open a single SQLite in-memory connection
var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite(connection));

builder.Services.AddMcpServer()
                .WithHttpTransport(o => o.Stateless = true)
                .WithToolsFromAssembly();

var app = builder.Build();

// Ensure the database schema is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.MapMcp("/mcp");
app.Run();