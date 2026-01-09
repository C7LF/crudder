namespace CrudderApi.DTOs.Labels
{
    public class CreateLabelRequest
    {
        public string Text { get; set; } = string.Empty;
        public string Colour { get; set; } = "#000000";
    }
}