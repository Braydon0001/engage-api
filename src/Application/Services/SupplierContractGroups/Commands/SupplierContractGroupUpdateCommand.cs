// auto-generated
namespace Engage.Application.Services.SupplierContractGroups.Commands;

public class SupplierContractGroupUpdateCommand : IMapTo<SupplierContractGroup>, IRequest<SupplierContractGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractGroupUpdateCommand, SupplierContractGroup>();
    }
}

public class SupplierContractGroupUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractGroupUpdateCommand, SupplierContractGroup>
{
    public SupplierContractGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractGroup> Handle(SupplierContractGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractGroups.SingleOrDefaultAsync(e => e.SupplierContractGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractGroupValidator : AbstractValidator<SupplierContractGroupUpdateCommand>
{
    public UpdateSupplierContractGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}