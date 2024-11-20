namespace Engage.Application.Services.ClaimFloats.Models;

public class ClaimFloatVm : IMapFrom<ClaimFloat>
{
    public int Id { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public OptionDto SupplierId { get; set; }
    public decimal Amount { get; set; }
    public decimal MinimumAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {

        profile.CreateMap<ClaimFloat, ClaimFloatVm>()
            .ForMember(d => d.Id, opts => opts.MapFrom(d => d.ClaimFloatId))
            .ForMember(d => d.EngageRegionId, opts => opts.MapFrom(d => new OptionDto(d.EngageRegionId, d.EngageRegion.Name)))
            .ForMember(d => d.SupplierId, opts => opts.MapFrom(d => new OptionDto(d.SupplierId, d.Supplier.Name)));
    }
}
