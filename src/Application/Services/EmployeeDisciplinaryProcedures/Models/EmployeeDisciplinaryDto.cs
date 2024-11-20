namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Models;

public class EmployeeDisciplinaryDto : IMapFrom<EmployeeDisciplinaryProcedure>
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime DisciplinaryProcedureDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeDisciplinaryProcedure, EmployeeDisciplinaryDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmployeeDisciplinaryProcedureId))
            .ForMember(e => e.Files, opt => opt.MapFrom(d => d.Files));
    }
}
