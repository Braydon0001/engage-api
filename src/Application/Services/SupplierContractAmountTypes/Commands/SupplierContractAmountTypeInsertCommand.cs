// auto-generated
namespace Engage.Application.Services.SupplierContractAmountTypes.Commands;

public class SupplierContractAmountTypeInsertCommand : IMapTo<SupplierContractAmountType>, IRequest<SupplierContractAmountType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractAmountTypeInsertCommand, SupplierContractAmountType>();
    }
}

public class SupplierContractAmountTypeInsertHandler : InsertHandler, IRequestHandler<SupplierContractAmountTypeInsertCommand, SupplierContractAmountType>
{
    public SupplierContractAmountTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractAmountType> Handle(SupplierContractAmountTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierContractAmountTypeInsertCommand, SupplierContractAmountType>(command);
        
        _context.SupplierContractAmountTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractAmountTypeInsertValidator : AbstractValidator<SupplierContractAmountTypeInsertCommand>
{
    public SupplierContractAmountTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}