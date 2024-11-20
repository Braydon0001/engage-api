namespace Engage.Application.Services.EmployeeKpiScores.Commands;

public class EmployeeKpiScoreInsertCommand : IMapTo<EmployeeKpiScore>, IRequest<EmployeeKpiScore>
{
    public int EmployeeId { get; set; }
    public int EmployeeKpiId { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public float Score { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpiScoreInsertCommand, EmployeeKpiScore>();
    }
}

public record EmployeeKpiScoreInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeKpiScoreInsertCommand, EmployeeKpiScore>
{
    public async Task<EmployeeKpiScore> Handle(EmployeeKpiScoreInsertCommand command, CancellationToken cancellationToken)
    {
        var existingScore = await Context.EmployeeKpiScores
                                                .Where(c => c.EmployeeId == command.EmployeeId
                                                    && c.EmployeeKpiId == command.EmployeeKpiId)
                                                .FirstOrDefaultAsync();

        if (existingScore != null)
        {
            throw new Exception("Employee Already has a score for this KPI");
        }

        var entity = Mapper.Map<EmployeeKpiScoreInsertCommand, EmployeeKpiScore>(command);

        Context.EmployeeKpiScores.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeKpiScoreInsertValidator : AbstractValidator<EmployeeKpiScoreInsertCommand>
{
    public EmployeeKpiScoreInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeKpiId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.Score).NotEmpty();
    }
}