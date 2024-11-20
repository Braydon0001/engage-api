namespace Engage.Domain.Entities.Json;

public class JsonRule
{
    public JsonRule()
    {
    }

    public JsonRule(string type, string value)
    {
        Type = type;
        Value = value;
    }
    public string Value { get; set; }
    public string Type { get; set; }
}
