namespace Engage.Application.Services.FileTypes.Queries;

public class FileTypeOption : OptionDto, IMapFrom<FileType>
{
    public bool IsUrl { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileType, FileTypeOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FileTypeId));
    }
}
