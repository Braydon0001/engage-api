namespace Engage.Application.Services.ClaimTypes.Commands;

public class ClaimTypeCommand : IMapTo<ClaimType>
{
    public string Name { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool IsDairy { get; set; }
    public int VatId { get; set; }
    public bool IsEmployeeClaim { get; set; }
    public int? SupplierId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimTypeCommand, ClaimType>();
    }
}
