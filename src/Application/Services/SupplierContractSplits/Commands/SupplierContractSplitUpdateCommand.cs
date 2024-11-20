// auto-generated
namespace Engage.Application.Services.SupplierContractSplits.Commands;

public class SupplierContractSplitUpdateCommand : IMapTo<SupplierContractSplit>, IRequest<SupplierContractSplit>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSplitUpdateCommand, SupplierContractSplit>();
    }
}

public class SupplierContractSplitUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractSplitUpdateCommand, SupplierContractSplit>
{
    public SupplierContractSplitUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractSplit> Handle(SupplierContractSplitUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractSplits.SingleOrDefaultAsync(e => e.SupplierContractSplitId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractSplitValidator : AbstractValidator<SupplierContractSplitUpdateCommand>
{
    public UpdateSupplierContractSplitValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}