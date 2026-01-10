using Crudder.Application.Labels.Dtos;

namespace Crudder.Api.Dtos.Todo;

public class TodoResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool Completed { get; set; }

    public List<LabelResponse> Labels { get; set; } = [];
}
