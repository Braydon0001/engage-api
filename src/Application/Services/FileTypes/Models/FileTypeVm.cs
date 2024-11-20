namespace Engage.Application.Services.FileTypes.Models;

public class FileTypeVm : IMapFrom<FileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public bool CanView { get; set; }
    public bool IsUrl { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileType, FileTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FileTypeId));
    }
}
