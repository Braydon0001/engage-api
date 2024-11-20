namespace Engage.Application.Services.Budgets.Commands;

class BudgetValidator<T> : AbstractValidator<T> where T : BudgetCommand
{
    public BudgetValidator()
    {
        RuleFor(x => x.GLAccountId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.BudgetTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.BudgetYearId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.BudgetVersionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.BudgetPeriodId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Value).GreaterThanOrEqualTo(0).NotEmpty();
    }

    public class CreateBudgetValidator : BudgetValidator<CreateBudgetCommand>
    {
        public CreateBudgetValidator()
        {

        }
    }

    public class UpdateBudgetValidator : BudgetValidator<UpdateBudgetCommand>
    {
        public UpdateBudgetValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
