namespace CrudderApi.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Colour { get; set; } = "#000000";
        
        // Relations
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        
        public ICollection<TodoItem> Todos { get; set; } = [];
    }
}