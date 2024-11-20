namespace Engage.Application.Services.BudgetPeriods.Commands;

class BudgetPeriodValidator<T> : AbstractValidator<T> where T : BudgetPeriodCommand
{
    public BudgetPeriodValidator()
    {
        RuleFor(x => x.BudgetYearId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }

    public class CreateBudgetPeriodValidator : BudgetPeriodValidator<CreateBudgetPeriodCommand>
    {
        public CreateBudgetPeriodValidator()
        {

        }
    }

    public class UpdateBudgetPeriodValidator : BudgetPeriodValidator<UpdateBudgetPeriodCommand>
    {
        public UpdateBudgetPeriodValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
