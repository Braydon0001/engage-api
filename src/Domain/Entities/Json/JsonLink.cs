namespace Engage.Domain.Entities.Json;

public class JsonLink
{
    public JsonLink()
    {

    }
    public JsonLink(string url)
    {
        Url = url;
    }
    public string Url { get; set; }
}
