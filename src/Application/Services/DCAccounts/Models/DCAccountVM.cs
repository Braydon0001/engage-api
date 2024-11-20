namespace Engage.Application.Services.DCAccounts.Models;

public class DCAccountVm : IMapFrom<DCAccount>
{
    public int Id { get; set; }
    public OptionDto StoreId { get; set; }
    public OptionDto DistributionCenterId { get; set; }
    public string AccountNumber { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrimary { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCAccount, DCAccountVm>()
            .ForMember(d => d.Id, opts => opts.MapFrom(s => s.DCAccountId))
            .ForMember(d => d.StoreId, opts => opts.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)))
            .ForMember(d => d.DistributionCenterId, opts => opts.MapFrom(s => new OptionDto { Id = s.DistributionCenterId, Name = s.DistributionCenter.Name }));

    }
}
