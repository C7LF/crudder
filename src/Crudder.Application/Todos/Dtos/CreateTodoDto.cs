namespace Crudder.Application.Todos.Dtos;

public class CreateTodoDto
{
    public string Title { get; set; } = string.Empty;
    public List<int> LabelIds { get; set; } = [];
}