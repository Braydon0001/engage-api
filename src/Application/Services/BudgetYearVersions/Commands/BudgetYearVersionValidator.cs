namespace Engage.Application.Services.BudgetYearVersions.Commands;

class BudgetYearVersionValidator<T> : AbstractValidator<T> where T : BudgetYearVersionCommand
{
    public BudgetYearVersionValidator()
    {
        RuleFor(x => x.BudgetYearId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.BudgetVersionId).GreaterThan(0).NotEmpty();
    }

    public class CreateBudgetYearVersionValidator : BudgetYearVersionValidator<CreateBudgetYearVersionCommand>
    {
        public CreateBudgetYearVersionValidator()
        {

        }
    }

    public class UpdateBudgetYearVersionValidator : BudgetYearVersionValidator<UpdateBudgetYearVersionCommand>
    {
        public UpdateBudgetYearVersionValidator()
        {

        }
    }
}
