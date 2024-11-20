namespace Engage.Application.Services.StoreStoreConcepts.Models;

public class StoreConceptsGroup
{
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public Dictionary<string, int> Concepts { get; set; }
}
