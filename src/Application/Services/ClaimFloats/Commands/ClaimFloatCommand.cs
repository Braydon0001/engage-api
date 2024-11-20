namespace Engage.Application.Services.ClaimFloats.Commands;

public class ClaimFloatCommand : IMapTo<ClaimFloat>
{
    public int EngageRegionId { get; set; }
    public int SupplierId { get; set; }
    public decimal Amount { get; set; }
    public decimal MinimumAmount { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimFloatCommand, ClaimFloat>();
    }
}
