namespace Engage.Application.Services.StoreCycles.Queries;

public class StoreCycleDto : IMapFrom<StoreCycle>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int EngageDepartmentId { get; set; }
    public string EngageDepartmentName { get; set; }
    public int StoreCycleOperationId { get; set; }
    public string StoreCycleOperationName { get; set; }
    public int FrequencyTypeId { get; set; }
    public string FrequencyTypeName { get; set; }
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
        profile.CreateMap<StoreCycle, StoreCycleDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreCycleId));
    }
}
