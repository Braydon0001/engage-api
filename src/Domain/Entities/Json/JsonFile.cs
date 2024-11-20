namespace Engage.Domain.Entities.Json;

public class JsonFile
{
    public JsonFile()
    {

    }
    public JsonFile(string url)
    {
        Url = url;
    }
    public JsonFile(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public JsonFile(string name, string url, string type)
    {
        Name = name;
        Url = url;
        Type = type;
    }

    public string Name { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
}
