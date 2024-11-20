// auto-generated
namespace Engage.Application.Services.NPrintingBatches.Queries;

public class NPrintingBatchDto : IMapFrom<NPrintingBatch>
{
    public int Id { get; set; }
    public int WebFileCategoryId { get; set; }
    public string WebFileCategoryName { get; set; }
    public string WebFileGroupName { get; set; }
    public int FileTypeId { get; set; }
    public string FileTypeName { get; set; }
    public string Report { get; set; }
    public string DisplayName { get; set; }
    public DateTime Created { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<NPrintingBatch, NPrintingBatchDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.NPrintingBatchId))
               .ForMember(d => d.WebFileGroupName, opt => opt.MapFrom(s => s.WebFileCategory.WebFileGroup.Name));
    }
}
