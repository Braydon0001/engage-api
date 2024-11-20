namespace Engage.Application.Services.Vouchers.Queries
{
    public static class VoucherPaginationProps
    {

        public static Dictionary<string, PaginationProperty> Create()
        {
            return new()
        {
            {"id", new PaginationProperty("VoucherId") },
            {"name", new PaginationProperty("Name") },
            {"supplierName", new PaginationProperty("Supplier.Name") },
            {"engageRegionName", new PaginationProperty("EngageRegionId", sortProperty: "EngageRegion.Name") },
            {"voucherStatusName", new PaginationProperty("VoucherStatusId", sortProperty: "VoucherStatus.Name") },
            {"createdBy", new PaginationProperty("CreatedBy") },
            {"closedBy", new PaginationProperty("ClosedBy") },
            {"createdDate", new PaginationProperty("CreatedDate.Date") },
            {"closedDate", new PaginationProperty("ClosedDate.Value.Date") },
        };
        }
    }
}
