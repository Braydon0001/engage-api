namespace Engage.Application.Services.EmployeeStoreKpiScores.Commands;

public class EmployeeStoreKpiScoreInsertCommand : IMapTo<EmployeeStoreKpiScore>, IRequest<EmployeeStoreKpiScore>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int EmployeeKpiId { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public float Score { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreKpiScoreInsertCommand, EmployeeStoreKpiScore>();
    }
}

public record EmployeeStoreKpiScoreInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeStoreKpiScoreInsertCommand, EmployeeStoreKpiScore>
{
    public async Task<EmployeeStoreKpiScore> Handle(EmployeeStoreKpiScoreInsertCommand command, CancellationToken cancellationToken)
    {
        var existingScore = await Context.EmployeeStoreKpiScores
                                                .Where(c => c.EmployeeId == command.EmployeeId
                                                    && c.EmployeeKpiId == command.EmployeeKpiId
                                                    && c.StoreId == command.StoreId)
                                                .FirstOrDefaultAsync();

        if (existingScore != null)
        {
            throw new Exception("Employee Already has a score for this KPI in the selected Store");
        }
        var entity = Mapper.Map<EmployeeStoreKpiScoreInsertCommand, EmployeeStoreKpiScore>(command);

        Context.EmployeeStoreKpiScores.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeStoreKpiScoreInsertValidator : AbstractValidator<EmployeeStoreKpiScoreInsertCommand>
{
    public EmployeeStoreKpiScoreInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeKpiId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.Score).NotEmpty();
    }
}