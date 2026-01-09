namespace Crudder.Application.Todos.Dtos;

public class UpdateTodoDto
{
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public bool? Completed { get; set; }
    public List<int>? LabelIds { get; set; }
}