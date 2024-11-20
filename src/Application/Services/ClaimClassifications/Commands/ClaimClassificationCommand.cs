namespace Engage.Application.Services.ClaimClassifications.Commands;

public class ClaimClassificationCommand : IMapTo<ClaimClassification>
{
    public string Name { get; set; }
    public bool IsPayStore { get; set; }
    public bool EditIsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public bool EditIsClaimFromSupplier { get; set; }
    public bool IsDairy { get; set; }
    public bool IsSupplierProcess { get; set; }
    public int? ClaimTypeId { get; set; }
    public int? SupplierId { get; set; }
    public bool IsAttachmentRequiredOnSubmit { get; set; }
    public List<int> ClaimTypeIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimClassificationCommand, ClaimClassification>();
    }
}
