namespace Engage.Domain.Entities.Json;

public class JsonText
{
    public JsonText()
    {

    }

    public JsonText(string type, string align, List<JsonText> children)
    {
        Type = type;
        Align = align;
        Children = children;
    }
    public JsonText(string text, bool bold = false, bool italics = false, bool underline = false, bool code = false)
    {
        Text = text;
        Bold = bold;
        Italic = italics;
        Underline = underline;
        Code = code;
    }

    public string Type { get; set; }
    public List<JsonText> Children { get; set; }
    public string Text { get; set; }
    public string Align { get; set; }
    public bool Bold { get; set; }
    public bool Italic { get; set; }
    public bool Underline { get; set; }
    public bool Code { get; set; }
}
