namespace CrudderApi.DTOs.Todo
{
    public class UpdateTodoRequest
    {
        public string? Title { get; set; } = string.Empty;
        public bool? Completed { get; set; }
        public List<int>? LabelIds { get; set; }
    }
}