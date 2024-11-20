namespace Engage.Application.Services.VatPeriods.Models;

public class VatPeriodVm : IMapFrom<VatPeriod>
{
    public int Id { get; set; }
    public OptionDto VatId { get; set; }
    public DateTime StartDate { get; set; }
    public int Percent { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VatPeriod, VatPeriodVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VatPeriodId))
            .ForMember(d => d.VatId, opt => opt.MapFrom(s => new OptionDto(s.VatId, s.Vat.Name)));
    }
}
