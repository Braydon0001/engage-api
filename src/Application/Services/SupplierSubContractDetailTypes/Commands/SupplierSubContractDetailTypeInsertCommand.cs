namespace Engage.Application.Services.SupplierSubContractDetailTypes.Commands;

public class SupplierSubContractDetailTypeInsertCommand : IMapTo<SupplierSubContractDetailType>, IRequest<SupplierSubContractDetailType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetailTypeInsertCommand, SupplierSubContractDetailType>();
    }
}
public class SupplierSubContractDetailTypeInsertHandler : InsertHandler, IRequestHandler<SupplierSubContractDetailTypeInsertCommand, SupplierSubContractDetailType>
{
    public SupplierSubContractDetailTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractDetailType> Handle(SupplierSubContractDetailTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierSubContractDetailTypeInsertCommand, SupplierSubContractDetailType>(command);

        _context.SupplierSubContractDetailTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
public class SupplierSubContractDetailTypeInsertValidator : AbstractValidator<SupplierSubContractDetailTypeInsertCommand>
{
    public SupplierSubContractDetailTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}