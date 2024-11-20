namespace Engage.Application.Services.ClaimFloats.Queries;

public static class ClaimFloatPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {

        return new() {
            {"id", new("ClaimFloatId") },
            {"reference", new("Reference") },
            {"amount", new("Amount") },
            {"supplierName", new("Supplier.Name") },
            {"engageRegionName", new("EngageRegionId", sortProperty: "EngageRegion.Name") },
            {"startDate", new("StartDate.Value.Date") },
            {"endDate", new("EndDate.Value.Date") },
            {"lastToppedUp", new("LastToppedUp.Value.Date") },
        };
    }
}
