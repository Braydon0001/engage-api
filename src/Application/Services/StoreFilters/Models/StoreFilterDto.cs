namespace Engage.Application.Services.StoreFilters.Models;

public class StoreFilterDto : IMapFrom<StoreFilter>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int FileUploadId { get; set; }
    public string FileUploadName { get; set; }
    public string StoreName { get; set; }
    public string Filter { get; set; }
    public string AS400 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreFilter, StoreFilterDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreFilterId))
            .ForMember(d => d.FileUploadName, opt => opt.MapFrom(s => s.FileUpload.FileName));
    }
}
