// auto-generated
namespace Engage.Application.Services.SupplierBudgetTypes.Commands;

public class SupplierBudgetTypeInsertCommand : IMapTo<SupplierBudgetType>, IRequest<SupplierBudgetType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetTypeInsertCommand, SupplierBudgetType>();
    }
}

public class SupplierBudgetTypeInsertHandler : InsertHandler, IRequestHandler<SupplierBudgetTypeInsertCommand, SupplierBudgetType>
{
    public SupplierBudgetTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetType> Handle(SupplierBudgetTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierBudgetTypeInsertCommand, SupplierBudgetType>(command);
        
        _context.SupplierBudgetTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierBudgetTypeInsertValidator : AbstractValidator<SupplierBudgetTypeInsertCommand>
{
    public SupplierBudgetTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}