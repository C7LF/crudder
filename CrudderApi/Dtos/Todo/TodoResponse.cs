namespace CrudderApi.DTOs.Todo
{
    public class TodoResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}