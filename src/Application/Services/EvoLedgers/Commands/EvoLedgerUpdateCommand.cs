namespace Engage.Application.Services.EvoLedgers.Commands;

public class EvoLedgerUpdateCommand : IMapTo<EvoLedger>, IRequest<EvoLedger>
{
    public int Id { get; set; }
    public string LedgerCode { get; init; }
    public string Name { get; init; }
    public int AnalysisPillarSubGroupId { get; init; }
    public bool IsActive { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EvoLedgerUpdateCommand, EvoLedger>();
    }
}

public record EvoLedgerUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EvoLedgerUpdateCommand, EvoLedger>
{
    public async Task<EvoLedger> Handle(EvoLedgerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EvoLedgers.SingleOrDefaultAsync(e => e.EvoLedgerId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEvoLedgerValidator : AbstractValidator<EvoLedgerUpdateCommand>
{
    public UpdateEvoLedgerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.LedgerCode).NotEmpty().MaximumLength(20);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.AnalysisPillarSubGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.IsActive);
    }
}