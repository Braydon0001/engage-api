// auto-generated
namespace Engage.Application.Services.SupplierContractSplits.Commands;

public class SupplierContractSplitInsertCommand : IMapTo<SupplierContractSplit>, IRequest<SupplierContractSplit>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSplitInsertCommand, SupplierContractSplit>();
    }
}

public class SupplierContractSplitInsertHandler : InsertHandler, IRequestHandler<SupplierContractSplitInsertCommand, SupplierContractSplit>
{
    public SupplierContractSplitInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractSplit> Handle(SupplierContractSplitInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierContractSplitInsertCommand, SupplierContractSplit>(command);
        
        _context.SupplierContractSplits.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractSplitInsertValidator : AbstractValidator<SupplierContractSplitInsertCommand>
{
    public SupplierContractSplitInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}