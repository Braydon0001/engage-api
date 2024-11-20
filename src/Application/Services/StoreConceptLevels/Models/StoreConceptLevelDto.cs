namespace Engage.Application.Services.StoreConceptLevels.Models;

public class StoreConceptLevelDto : IMapFrom<StoreConceptLevel>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int StoreConceptId { get; set; }
    public string StoreConceptName { get; set; }
    public int Level { get; set; }
    public string BlobUrl { get; set; }
    public string BlobName { get; set; }
    public int Target { get; set; }
    public int Actual { get; set; }
    public double Score { get; set; }
    public List<FileDto> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptLevel, StoreConceptLevelDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreConceptLevelId))
           .ForMember(d => d.Files, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.BlobUrl) && !string.IsNullOrWhiteSpace(s.BlobName)
                                                        ? new List<FileDto> { new FileDto { Name = s.BlobName, Url = s.BlobUrl } }
                                                        : null));
    }
}
