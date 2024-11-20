namespace Engage.Application.Services.Vacancies.Commands;

class VacancyValidator<T> : AbstractValidator<T> where T : VacancyCommand
{
    public VacancyValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(220);
        RuleFor(x => x.StartDate).NotEmpty();
    }

    public class CreateVacancyValidator : VacancyValidator<CreateVacancyCommand>
    {
        public CreateVacancyValidator()
        {

        }
    }

    public class UpdateVacancyValidator : VacancyValidator<UpdateVacancyCommand>
    {
        public UpdateVacancyValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
