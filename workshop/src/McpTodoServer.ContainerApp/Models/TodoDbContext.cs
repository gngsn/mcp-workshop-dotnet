using Microsoft.EntityFrameworkCore;

namespace McpTodoServer.ContainerApp.Models;

public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().HasKey(t => t.ID);
    }
}
