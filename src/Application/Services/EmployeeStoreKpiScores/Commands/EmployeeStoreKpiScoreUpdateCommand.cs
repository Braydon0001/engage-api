namespace Engage.Application.Services.EmployeeStoreKpiScores.Commands;

public class EmployeeStoreKpiScoreUpdateCommand : IMapTo<EmployeeStoreKpiScore>, IRequest<EmployeeStoreKpiScore>
{
    public int Id { get; init; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int EmployeeKpiId { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public float Score { get; set; }
    public bool SaveChanges { get; set; } = true;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreKpiScoreUpdateCommand, EmployeeStoreKpiScore>();
    }
}

public record EmployeeStoreKpiScoreUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeStoreKpiScoreUpdateCommand, EmployeeStoreKpiScore>
{
    public async Task<EmployeeStoreKpiScore> Handle(EmployeeStoreKpiScoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EmployeeStoreKpiScores.SingleOrDefaultAsync(e => e.EmployeeStoreKpiScoreId == command.Id, cancellationToken);
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

public class UpdateEmployeeStoreKpiScoreValidator : AbstractValidator<EmployeeStoreKpiScoreUpdateCommand>
{
    public UpdateEmployeeStoreKpiScoreValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeKpiId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Score).NotEmpty();
    }
}