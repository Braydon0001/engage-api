// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersionTypes.Commands;

public class SupplierBudgetVersionTypeInsertCommand : IMapTo<SupplierBudgetVersionType>, IRequest<SupplierBudgetVersionType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersionTypeInsertCommand, SupplierBudgetVersionType>();
    }
}

public class SupplierBudgetVersionTypeInsertHandler : InsertHandler, IRequestHandler<SupplierBudgetVersionTypeInsertCommand, SupplierBudgetVersionType>
{
    public SupplierBudgetVersionTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetVersionType> Handle(SupplierBudgetVersionTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierBudgetVersionTypeInsertCommand, SupplierBudgetVersionType>(command);
        
        _context.SupplierBudgetVersionTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierBudgetVersionTypeInsertValidator : AbstractValidator<SupplierBudgetVersionTypeInsertCommand>
{
    public SupplierBudgetVersionTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}