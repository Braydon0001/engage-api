namespace Engage.Application.Services.StoreBankDetails.Commands;

public class StoreBankDetailCommand : IMapTo<StoreBankDetail>
{
    public string Bank { get; set; }
    public string BranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public bool IsPrimary { get; set; }
    public string Note { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<StoreBankDetailCommand, StoreBankDetail>();
    }
}
