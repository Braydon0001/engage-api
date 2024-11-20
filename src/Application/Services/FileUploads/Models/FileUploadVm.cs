namespace Engage.Application.Services.FileUploads.Models;

public class FileUploadDto : IMapFrom<FileUpload>
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public DateTime? ImportDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileUpload, FileUploadDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FileUploadId));
    }
}
