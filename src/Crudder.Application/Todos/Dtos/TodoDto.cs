using Crudder.Application.Labels.Dtos;

namespace Crudder.Application.Todos.Dtos;

public class TodoDto(int id, string title, string? content, bool completed, List<LabelDto> labels)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string? Content { get; set; } = content;
    public bool Completed { get; set; } = completed;
    
    public List<LabelDto> Labels { get; set; } = labels;
}
