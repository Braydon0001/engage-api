using Engage.Application.Services.EngageDepartments.Models;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.StoreCycles.Queries;

public class StoreCycleVm : IMapFrom<StoreCycle>
{
    public int Id { get; set; }
    public StoreOption StoreId { get; set; }
    public EngageDepartmentOption EngageDepartmentId { get; set; }
    public OptionDto StoreCycleOperationId { get; set; }
    public OptionDto FrequencyTypeId { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreCycle, StoreCycleVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreCycleId))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store))
               .ForMember(d => d.EngageDepartmentId, opt => opt.MapFrom(s => s.EngageDepartment))
               .ForMember(d => d.StoreCycleOperationId, opts => opts.MapFrom(s => new OptionDto { Id = s.StoreCycleOperationId, Name = s.StoreCycleOperation.Name }))
               .ForMember(d => d.FrequencyTypeId, opts => opts.MapFrom(s => new OptionDto { Id = s.FrequencyTypeId, Name = s.FrequencyType.Name }));
    }
}
