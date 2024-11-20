namespace Engage.Application.Services.FileUploads.Models;

public class FileUploadVm : IMapFrom<FileUpload>
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public DateTime? ImportDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileUpload, FileUploadVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FileUploadId));
    }
}
