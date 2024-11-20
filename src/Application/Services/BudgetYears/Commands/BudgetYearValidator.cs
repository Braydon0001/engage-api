namespace Engage.Application.Services.BudgetYears.Commands;

class BudgetYearValidator<T> : AbstractValidator<T> where T : BudgetYearCommand
{
    public BudgetYearValidator()
    {
        RuleFor(x => x.Name).MaximumLength(20).NotEmpty();
    }

    public class CreateBudgetYearValidator : BudgetYearValidator<CreateBudgetYearCommand>
    {
        public CreateBudgetYearValidator()
        {

        }
    }

    public class UpdateBudgetYearValidator : BudgetYearValidator<UpdateBudgetYearCommand>
    {
        public UpdateBudgetYearValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
