namespace Engage.Application.Models.Configuration
{
    //TODO Add Columns to Order Type
    public class OrderDefaultsOptions
    {
        public int SupplierId { get; set; }
        public int SkuTypeId { get; set; }
        public int SkuQuantityTypeId { get; set; }                
        public int DescriptionSkuTypeId { get; set; }
        public int DescriptionSkuQuantityTypeId { get; set; }
        public int DescriptionSkuDCProductId { get; set; }        
        public int ProductActiveStatusId { get; set; }
    }
}
