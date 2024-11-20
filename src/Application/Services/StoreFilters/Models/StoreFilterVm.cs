namespace Engage.Application.Services.StoreFilters.Models;

public class StoreFilterVm : IMapFrom<StoreFilter>
{
    public int Id { get; set; }
    public OptionDto StoreId { get; set; }
    public int FileUploadId { get; set; }
    public string FileUploadName { get; set; }
    public string Filter { get; set; }
    public string AS400 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreFilter, StoreFilterVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreFilterId))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)))
            .ForMember(d => d.FileUploadName, opt => opt.MapFrom(s => s.FileUpload.FileName));

    }
}
