namespace Engage.Application.Services.Shared.Models;

public class ListResult<T>
{
    public ListResult()
    {

    }

    public ListResult(List<T> data)
    {
        Data = data;
        Count = data.Count;
    }

    public object Count { get; set; }
    public List<T> Data { get; set; }


}
