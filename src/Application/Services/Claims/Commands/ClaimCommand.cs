namespace Engage.Application.Services.Claims.Commands;

public class ClaimCommand : IMapTo<Claim>
{
    public int ClientTypeId { get; set; }
    public int ClaimTypeId { get; set; }
    public int ClaimClassificationId { get; set; }
    public int VatId { get; set; }
    public int SupplierId { get; set; }
    public int StoreId { get; set; }
    public int DistributionCenterId { get; set; }
    public int? ClaimAccountManagerId { get; set; }
    public int? ClaimManagerId { get; set; }
    public int? ClaimFloatId { get; set; }
    public int? EmployeeDivisionId { get; set; }
    public string ClaimNumber { get; set; }
    public bool IsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool IsDairy { get; set; }
    public DateTime ClaimDate { get; set; }
    public string ClaimReference { get; set; }
    public string Comment { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimCommand, Claim>();
    }
}
