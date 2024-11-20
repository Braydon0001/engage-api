namespace Engage.Application.Services.StoreConceptLevels.Models;

public class StoreConceptLevelGroup
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public object Concepts { get; set; }
}
