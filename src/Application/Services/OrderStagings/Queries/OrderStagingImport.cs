namespace Engage.Application.Services.OrderStagings.Queries;

public class OrderStagingImport
{
    public string Region { get; set; }
    public string Store { get; set; }
    public string OrderNumber { get; set; }
    public string AccountNumber { get; set; }
    public string Address { get; set; }
    public string OrderContact { get; set; }
    public string OrderEmail { get; set; }
    public string VatNumber { get; set; }
    public string Product { get; set; }
    public string Barcode { get; set; }
    public string CaseType { get; set; }
    public int Quantity { get; set; }
    public string OrderDate { get; set; }
    public string Reference { get; set; }
    //public List<OrderStagingSkuImport> Skus { get; set; }
}

public class OrderStagingSkuImport
{
    public string Product { get; set; }
    public string Barcode { get; set; }
    public string CaseType { get; set; }
    public int Quantity { get; set; }
}