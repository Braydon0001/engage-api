
using Engage.Application.Services.Creditors.Queries;
using Engage.Application.Services.CreditorFileTypes.Queries;

namespace Engage.Application.Services.CreditorFiles.Queries;

public class CreditorFileVm : IMapFrom<CreditorFile>
{
    public int Id { get; init; }
    public CreditorOption CreditorId { get; init; }
    public CreditorFileTypeOption CreditorFileTypeId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFile, CreditorFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorFileId))
               .ForMember(d => d.CreditorId, opt => opt.MapFrom(s => s.Creditor))
               .ForMember(d => d.CreditorFileTypeId, opt => opt.MapFrom(s => s.CreditorFileType));
    }
}
