// auto-generated
namespace Engage.Application.Services.SupplierContractTypes.Commands;

public class SupplierContractTypeInsertCommand : IMapTo<SupplierContractType>, IRequest<SupplierContractType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractTypeInsertCommand, SupplierContractType>();
    }
}

public class SupplierContractTypeInsertHandler : InsertHandler, IRequestHandler<SupplierContractTypeInsertCommand, SupplierContractType>
{
    public SupplierContractTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractType> Handle(SupplierContractTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierContractTypeInsertCommand, SupplierContractType>(command);
        
        _context.SupplierContractTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractTypeInsertValidator : AbstractValidator<SupplierContractTypeInsertCommand>
{
    public SupplierContractTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}