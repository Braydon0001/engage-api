// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersions.Commands;

public class SupplierBudgetVersionUpdateCommand : IMapTo<SupplierBudgetVersion>, IRequest<SupplierBudgetVersion>
{
    public int Id { get; set; }
    public int SupplierPeriodId { get; set; }
    public int SupplierBudgetVersionTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersionUpdateCommand, SupplierBudgetVersion>();
    }
}

public class SupplierBudgetVersionUpdateHandler : UpdateHandler, IRequestHandler<SupplierBudgetVersionUpdateCommand, SupplierBudgetVersion>
{
    public SupplierBudgetVersionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetVersion> Handle(SupplierBudgetVersionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierBudgetVersions.SingleOrDefaultAsync(e => e.SupplierBudgetVersionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierBudgetVersionValidator : AbstractValidator<SupplierBudgetVersionUpdateCommand>
{
    public UpdateSupplierBudgetVersionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierBudgetVersionTypeId).NotEmpty().GreaterThan(0);
    }
}