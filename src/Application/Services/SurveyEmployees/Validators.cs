using FluentValidation;

namespace Engage.Application.Services.SurveyEmployees
{
    public class CreateSurveyEmployeesValidator : AbstractValidator<CreateSurveyEmployeesCommand>
    {
        public CreateSurveyEmployeesValidator()
        {
            RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Employees).NotEmpty();
            RuleForEach(x => x.Employees).GreaterThan(0);
        }
    }
    
    public class CreateSurveyEmployeesWithCriteriaValidator : AbstractValidator<CreateSurveyEmployeesWithCriteriaCommand>
    {
        public CreateSurveyEmployeesWithCriteriaValidator()
        {
            RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();            
            RuleForEach(x => x.EngageDepartments).GreaterThan(0);
            RuleForEach(x => x.JobTitles).GreaterThan(0);
        }
    }

    public class DeleteSurveyEmployeeValidator : AbstractValidator<DeleteSurveyEmployeeCommand>
    {
        public DeleteSurveyEmployeeValidator()
        {
            RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();            
        }
    }
}
