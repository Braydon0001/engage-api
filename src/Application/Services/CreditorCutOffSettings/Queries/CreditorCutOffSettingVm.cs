
namespace Engage.Application.Services.CreditorCutOffSettings.Queries;

public class CreditorCutOffSettingVm : IMapFrom<CreditorCutOffSetting>
{
    public int Id { get; init; }
    public string CreditorCutOff { get; set; }
    public string PaymentCutOff { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorCutOffSetting, CreditorCutOffSettingVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorCutOffSettingId));
    }
}
