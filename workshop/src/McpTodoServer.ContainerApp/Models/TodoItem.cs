namespace McpTodoServer.ContainerApp.Models;

public class TodoItem
{
    public int ID { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
