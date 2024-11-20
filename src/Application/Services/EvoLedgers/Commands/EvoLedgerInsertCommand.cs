namespace Engage.Application.Services.EvoLedgers.Commands;

public class EvoLedgerInsertCommand : IMapTo<EvoLedger>, IRequest<EvoLedger>
{
    public string LedgerCode { get; init; }
    public string Name { get; init; }
    public int AnalysisPillarSubGroupId { get; init; }
    public bool IsActive { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EvoLedgerInsertCommand, EvoLedger>();
    }
}

public record EvoLedgerInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EvoLedgerInsertCommand, EvoLedger>
{
    public async Task<EvoLedger> Handle(EvoLedgerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<EvoLedgerInsertCommand, EvoLedger>(command);

        Context.EvoLedgers.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EvoLedgerInsertValidator : AbstractValidator<EvoLedgerInsertCommand>
{
    public EvoLedgerInsertValidator()
    {
        RuleFor(e => e.LedgerCode).NotEmpty().MaximumLength(20);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.AnalysisPillarSubGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.IsActive);
    }
}