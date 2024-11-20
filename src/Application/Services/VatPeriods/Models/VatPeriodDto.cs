namespace Engage.Application.Services.VatPeriods.Models;

public class VatPeriodDto : IMapFrom<VatPeriod>
{
    public int Id { get; set; }
    public int VatId { get; set; }
    public string VatCode { get; set; }
    public string VatName { get; set; }
    public DateTime StartDate { get; set; }
    public int Percent { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VatPeriod, VatPeriodDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VatPeriodId))
            .ForMember(d => d.VatCode, opt => opt.MapFrom(s => s.Vat.Code))
            .ForMember(d => d.VatName, opt => opt.MapFrom(s => s.Vat.Name));
    }
}
