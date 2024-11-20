namespace Engage.Domain.Entities.Json;

public class JsonStoreConcept
{
    public JsonStoreConcept()
    {

    }

    public JsonStoreConcept(string name, string value, string type)
    {
        Name = name;
        Value = value;
        Type = type;
    }

    public string Name { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
}
