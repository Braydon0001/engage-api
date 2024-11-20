namespace Engage.Application.Models;

public class PdfModel<T> where T : class
{
    public T Data { get; set; }
    public string HeaderImageURL { get; set; }
    public Stream HeaderStream { get; set; }
    public HttpClient HttpClient { get; set; }
}
