// auto-generated
namespace Engage.Application.Services.SupplierContractAmountTypes.Commands;

public class SupplierContractAmountTypeUpdateCommand : IMapTo<SupplierContractAmountType>, IRequest<SupplierContractAmountType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractAmountTypeUpdateCommand, SupplierContractAmountType>();
    }
}

public class SupplierContractAmountTypeUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractAmountTypeUpdateCommand, SupplierContractAmountType>
{
    public SupplierContractAmountTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractAmountType> Handle(SupplierContractAmountTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractAmountTypes.SingleOrDefaultAsync(e => e.SupplierContractAmountTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractAmountTypeValidator : AbstractValidator<SupplierContractAmountTypeUpdateCommand>
{
    public UpdateSupplierContractAmountTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}