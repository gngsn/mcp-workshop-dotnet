using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using McpTodoServer.ContainerApp.Models;

namespace McpTodoServer.ContainerApp.Tools;

[McpServerToolType]
public class TodoTool
{
    private readonly TodoDbContext db;

    public TodoTool(TodoDbContext context)
    {
        db = context;
    }

    [McpServerTool(Name = "add_todo_item", Title = "Add a to-do item")]
    [Description("Adds a to-do item to database.")]
    public async Task<TodoItem> CreateAsync(string text)
    {
        var item = new TodoItem { Text = text, IsCompleted = false };
        db.TodoItems.Add(item);
        await db.SaveChangesAsync();
        return item;
    }

    [McpServerTool(Name = "get_todo_items", Title = "Get a list of to-do items")]
    [Description("Gets a list of to-do items from database.")]
    public async Task<List<TodoItem>> ListAsync()
    {
        return await db.TodoItems.ToListAsync();
    }

    [McpServerTool(Name = "update_todo_item", Title = "Update a to-do item")]
    [Description("Updates a to-do item in the database.")]
    public async Task<TodoItem?> UpdateAsync(int id, string text)
    {
        var item = await db.TodoItems.FindAsync(id);
        if (item == null) return null;
        item.Text = text;
        await db.SaveChangesAsync();
        return item;
    }

    [McpServerTool(Name = "complete_todo_item", Title = "Complete a to-do item")]
    [Description("Completes a to-do item in the database.")]
    public async Task<TodoItem?> CompleteAsync(int id)
    {
        var item = await db.TodoItems.FindAsync(id);
        if (item == null) return null;
        item.IsCompleted = true;
        await db.SaveChangesAsync();
        return item;
    }

    [McpServerTool(Name = "delete_todo_item", Title = "Delete a to-do item")]
    [Description("Deletes a to-do item from the database.")]
    public async Task<bool> DeleteAsync(int id)
    {
        var item = await db.TodoItems.FindAsync(id);
        if (item == null) return false;
        db.TodoItems.Remove(item);
        await db.SaveChangesAsync();
        return true;
    }
}
