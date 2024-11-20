namespace Engage.Application.Services.StoreBankDetails.Models;

public class StoreBankDetailVm : IMapFrom<StoreBankDetail>
{
    public int Id { get; set; }
    public OptionDto StoreId { get; set; }
    public string Bank { get; set; }
    public string BranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public bool IsPrimary { get; set; }
    public string Note { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreBankDetail, StoreBankDetailVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreBankDetailId))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)));
    }
}
