namespace Engage.Application.Services.Vacancies.Models;

public class VacancyDto : IMapFrom<Vacancy>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vacancy, VacancyDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.VacancyId));
    }
}
