namespace Engage.Application.Services.Vacancies.Commands;

public class VacancyCommand : IMapTo<Vacancy>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VacancyCommand, Vacancy>();
    }
}
