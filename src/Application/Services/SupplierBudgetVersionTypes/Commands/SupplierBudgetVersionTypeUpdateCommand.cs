// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersionTypes.Commands;

public class SupplierBudgetVersionTypeUpdateCommand : IMapTo<SupplierBudgetVersionType>, IRequest<SupplierBudgetVersionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersionTypeUpdateCommand, SupplierBudgetVersionType>();
    }
}

public class SupplierBudgetVersionTypeUpdateHandler : UpdateHandler, IRequestHandler<SupplierBudgetVersionTypeUpdateCommand, SupplierBudgetVersionType>
{
    public SupplierBudgetVersionTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetVersionType> Handle(SupplierBudgetVersionTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierBudgetVersionTypes.SingleOrDefaultAsync(e => e.SupplierBudgetVersionTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierBudgetVersionTypeValidator : AbstractValidator<SupplierBudgetVersionTypeUpdateCommand>
{
    public UpdateSupplierBudgetVersionTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}