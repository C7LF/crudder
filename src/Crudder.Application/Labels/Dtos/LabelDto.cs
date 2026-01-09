namespace Crudder.Application.Labels.Dtos;

public class LabelDto(int id, string text, string colour)
{
    public int Id { get; set; } = id;
    public string Text { get; set; } = text;
    public string Colour { get; set; } = colour;
}
