// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersions.Commands;

public class SupplierBudgetVersionInsertCommand : IMapTo<SupplierBudgetVersion>, IRequest<SupplierBudgetVersion>
{
    public int SupplierPeriodId { get; set; }
    public int SupplierBudgetVersionTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersionInsertCommand, SupplierBudgetVersion>();
    }
}

public class SupplierBudgetVersionInsertHandler : InsertHandler, IRequestHandler<SupplierBudgetVersionInsertCommand, SupplierBudgetVersion>
{
    public SupplierBudgetVersionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetVersion> Handle(SupplierBudgetVersionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierBudgetVersionInsertCommand, SupplierBudgetVersion>(command);
        
        _context.SupplierBudgetVersions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierBudgetVersionInsertValidator : AbstractValidator<SupplierBudgetVersionInsertCommand>
{
    public SupplierBudgetVersionInsertValidator()
    {
        RuleFor(e => e.SupplierPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierBudgetVersionTypeId).NotEmpty().GreaterThan(0);
    }
}