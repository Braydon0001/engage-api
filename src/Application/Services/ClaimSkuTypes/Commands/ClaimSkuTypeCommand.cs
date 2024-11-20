namespace Engage.Application.Services.ClaimSkuTypes.Commands;

public class ClaimSkuTypeCommand : IMapTo<ClaimSkuType>
{
    public string Name { get; set; }
    public bool IsVatInclusive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimSkuTypeCommand, ClaimSkuType>();
    }
}
