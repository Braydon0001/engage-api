// auto-generated
namespace Engage.Application.Services.SupplierContractDetailTypes.Commands;

public class SupplierContractDetailTypeInsertCommand : IMapTo<SupplierContractDetailType>, IRequest<SupplierContractDetailType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetailTypeInsertCommand, SupplierContractDetailType>();
    }
}

public class SupplierContractDetailTypeInsertHandler : InsertHandler, IRequestHandler<SupplierContractDetailTypeInsertCommand, SupplierContractDetailType>
{
    public SupplierContractDetailTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractDetailType> Handle(SupplierContractDetailTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierContractDetailTypeInsertCommand, SupplierContractDetailType>(command);
        
        _context.SupplierContractDetailTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractDetailTypeInsertValidator : AbstractValidator<SupplierContractDetailTypeInsertCommand>
{
    public SupplierContractDetailTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}