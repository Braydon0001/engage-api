using Engage.Application.Targetings;

namespace Engage.Application.Services.Targetings;

public class TargetingListDto<T>
{
    public int Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public string CriteriaString { get; set; }
    public T Criteria { get; set; }
}

public class EmployeeTargetingListDto : TargetingListDto<EmployeeTargetingCriteria>, IMapFrom<Targeting>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Targeting, EmployeeTargetingListDto>()
               .ForMember(d => d.Id, opts => opts.MapFrom(s => s.TargetingId))
               .ForMember(d => d.CriteriaString, opts => opts.MapFrom(s => s.Criteria))
               .ForMember(d => d.Criteria, opts => opts.Ignore());
    }
}

public class StoreTargetingListDto : TargetingListDto<StoreTargetingCriteria>, IMapFrom<Targeting>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Targeting, StoreTargetingListDto>()
               .ForMember(d => d.Id, opts => opts.MapFrom(s => s.TargetingId))
               .ForMember(d => d.CriteriaString, opts => opts.MapFrom(s => s.Criteria))
               .ForMember(d => d.Criteria, opts => opts.Ignore());
    }
}
