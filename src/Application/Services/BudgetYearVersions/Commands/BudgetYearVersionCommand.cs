namespace Engage.Application.Services.BudgetYearVersions.Commands;

public class BudgetYearVersionCommand : IMapTo<BudgetYearVersion>
{
    public int BudgetYearId { get; set; }
    public int BudgetVersionId { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetYearVersionCommand, BudgetYearVersion>();
    }
}
