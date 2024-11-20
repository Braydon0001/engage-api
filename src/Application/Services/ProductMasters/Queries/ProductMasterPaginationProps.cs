namespace Engage.Application.Services.ProductMasters.Queries;

public static class ProductMasterPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new() {

            { "id", new("ProductMasterId") },
            { "productBrandName", new("ProductBrandId", sortProperty: "ProductBrand.Name") },
            { "productReasonName", new("ProductReasonId", sortProperty: "ProductReason.Name") },
            { "productSubCategoryName", new("ProductSubCategoryId", sortProperty: "ProductSubCategory.Name") },
            { "productMasterStatusName", new("ProductMasterStatusId", sortProperty: "ProductMasterStatus.Name") },
            { "productMasterSystemStatusName", new("ProductMasterSystemStatusId", sortProperty: "ProductMasterSystemStatus.Name") },
            { "productVendorName", new("ProductVendorId", sortProperty: "ProductVendor.Name") },
            { "productManufacturerName", new("ProductManufacturerId", sortProperty: "ProductManufacturer.Name") },
            { "code", new("Code") },
            { "ag-Grid-AutoColumn", new("Name") },
            { "name", new("Name") },
            { "description", new("Description") },
            { "ledgerCode", new("LedgerCode") }

        };
    }
}
