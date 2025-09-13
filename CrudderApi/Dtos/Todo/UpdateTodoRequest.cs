namespace CrudderApi.DTOs.Todo
{
    public class UpdateTodoRequest
    {
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}