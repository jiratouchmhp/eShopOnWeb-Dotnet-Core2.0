namespace BlazorShared.Models;

public class SelectListItem
{
    public string Text { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool Selected { get; set; }
}