namespace Engage.Domain.Entities.Json;

public class JsonSetting
{
    public JsonSetting()
    {
    }

    public JsonSetting(string name, string value, string type)
    {
        Name = name;
        Value = value;
        Type = type;
    }

    public string Name { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
}
