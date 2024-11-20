namespace Engage.Application.Services.NPrintings.Queries;

public class NPrintingDto : IMapFrom<NPrinting>
{
    public int Id { get; set; }
    public int NPrintingBatchId { get; set; }
    public string FileName { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string Error { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<NPrinting, NPrintingDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.NPrintingId));
    }
}
