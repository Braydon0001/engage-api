namespace Engage.Application.Services.EmployeeKpiScores.Commands;

public class EmployeeKpiScoreUpdateCommand : IMapTo<EmployeeKpiScore>, IRequest<EmployeeKpiScore>
{
    public int Id { get; init; }
    public int EmployeeId { get; set; }
    public int EmployeeKpiId { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public float Score { get; set; }
    public bool SaveChanges { get; set; } = true;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpiScoreUpdateCommand, EmployeeKpiScore>();
    }
}

public record EmployeeKpiScoreUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeKpiScoreUpdateCommand, EmployeeKpiScore>
{
    public async Task<EmployeeKpiScore> Handle(EmployeeKpiScoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EmployeeKpiScores.SingleOrDefaultAsync(e => e.EmployeeKpiScoreId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        if (command.SaveChanges)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        return mappedEntity;
    }
}

public class UpdateEmployeeKpiScoreValidator : AbstractValidator<EmployeeKpiScoreUpdateCommand>
{
    public UpdateEmployeeKpiScoreValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeKpiId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Score).NotEmpty();
    }
}