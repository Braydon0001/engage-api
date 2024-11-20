// auto-generated
namespace Engage.Application.Services.SupplierBudgetTypes.Commands;

public class SupplierBudgetTypeUpdateCommand : IMapTo<SupplierBudgetType>, IRequest<SupplierBudgetType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetTypeUpdateCommand, SupplierBudgetType>();
    }
}

public class SupplierBudgetTypeUpdateHandler : UpdateHandler, IRequestHandler<SupplierBudgetTypeUpdateCommand, SupplierBudgetType>
{
    public SupplierBudgetTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetType> Handle(SupplierBudgetTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierBudgetTypes.SingleOrDefaultAsync(e => e.SupplierBudgetTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierBudgetTypeValidator : AbstractValidator<SupplierBudgetTypeUpdateCommand>
{
    public UpdateSupplierBudgetTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}