namespace Engage.Domain.Entities.Json;

public class JsonEmployeeField
{
    public JsonEmployeeField()
    {

    }

    public JsonEmployeeField(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
