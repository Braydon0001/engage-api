namespace Engage.Application.Services.Claims.Queries;

public static class ClaimPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new PaginationProperty("ClaimId") },
            {"storeName", new PaginationProperty("Store.Name")},
            {"supplierName", new PaginationProperty("Supplier.Name") },
            {"claimTypeName", new PaginationProperty("ClaimTypeId", sortProperty: "ClaimType.Name") },
            {"claimClassificationName", new PaginationProperty("ClaimClassificationId", sortProperty: "ClaimClassification.Name") },
            {"claimStatusName", new PaginationProperty("ClaimStatusId", sortProperty: "ClaimStatus.Name") },
            {"claimSupplierStatusName", new PaginationProperty("ClaimSupplierStatusId", sortProperty: "ClaimSupplierStatus.Name") },
            {"engageRegionName", new PaginationProperty("Store.EngageRegionId", sortProperty: "Store.EngageRegion.Name") },
            {"claimAccountManagerName", new PaginationProperty("ClaimAccountManager.FullName") },
            {"claimManagerName", new PaginationProperty("ClaimManager.FullName") },
            {"claimNumber", new PaginationProperty("ClaimNumber") },
            {"claimReference", new PaginationProperty("ClaimReference") },
            {"createdBy", new PaginationProperty("CreatedBy") },
            {"approvedBy", new PaginationProperty("ApprovedBy") },
            {"claimDate", new PaginationProperty("ClaimDate.Date") },
            {"createdDate", new PaginationProperty("Created.Date") },
            {"approvedDate", new PaginationProperty("ApprovedDate.Value.Date")},
            {"paidDate", new PaginationProperty("PaidDate.Value.Date") },
            {"TotalAmountSubTotal", new PaginationProperty("PaidDate.Value.Date") },
        };
    }

}
