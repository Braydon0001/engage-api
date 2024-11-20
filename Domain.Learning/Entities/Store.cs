namespace Domain.Learning.Entities;

public class Store
{
    public int StoreId { get; set; }
    public int? ApiStoreId { get; set; }
    public int? AccountId { get; set; }
    public int? RegionId { get; set; }
    public int? DivisionId { get; set; }
    public string RegionName { get; set; }
    public string DivisionName { get; set; }
    public string StoreName { get; set; }
    public bool IsActive { get; set; }
    public string Country { get; set; }
    public bool IsElearningStore { get; set; }
    public DateTime DateCreated { get; set; }
    public string ExternalCode { get; set; }
    public string Pin { get; set; }
    public string Code { get; set; }

    public Region Region { get; set; }
    //public Account Account { get; set; }
    //public Division Division { get; set; }
}
