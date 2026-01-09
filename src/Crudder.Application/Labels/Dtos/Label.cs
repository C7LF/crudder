namespace CrudderApi.DTOs.Labels
{
    public class LabelResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Colour { get; set; } = "#000000";
    }
}
