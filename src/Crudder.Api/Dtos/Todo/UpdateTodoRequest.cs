namespace Crudder.Api.Dtos.Todo;

public class UpdateTodoRequest
{
    public string? Title { get; set; } = string.Empty;
    public bool? Completed { get; set; }
    public string? Content { get; set; } = string.Empty;

    public List<int>? LabelIds { get; set; }
}
