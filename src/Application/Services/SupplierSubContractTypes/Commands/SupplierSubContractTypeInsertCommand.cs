// auto-generated
namespace Engage.Application.Services.SupplierSubContractTypes.Commands;

public class SupplierSubContractTypeInsertCommand : IMapTo<SupplierSubContractType>, IRequest<SupplierSubContractType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractTypeInsertCommand, SupplierSubContractType>();
    }
}

public class SupplierSubContractTypeInsertHandler : InsertHandler, IRequestHandler<SupplierSubContractTypeInsertCommand, SupplierSubContractType>
{
    public SupplierSubContractTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractType> Handle(SupplierSubContractTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierSubContractTypeInsertCommand, SupplierSubContractType>(command);
        
        _context.SupplierSubContractTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierSubContractTypeInsertValidator : AbstractValidator<SupplierSubContractTypeInsertCommand>
{
    public SupplierSubContractTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}