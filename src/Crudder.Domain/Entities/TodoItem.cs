namespace Crudder.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content {get; set; } = string.Empty;
        public bool Completed { get; set; }
        public string Label { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Label> Labels { get; set; } = [];

    }
}