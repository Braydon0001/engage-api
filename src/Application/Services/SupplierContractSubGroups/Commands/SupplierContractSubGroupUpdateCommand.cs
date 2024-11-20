// auto-generated
namespace Engage.Application.Services.SupplierContractSubGroups.Commands;

public class SupplierContractSubGroupUpdateCommand : IMapTo<SupplierContractSubGroup>, IRequest<SupplierContractSubGroup>
{
    public int Id { get; set; }
    public int SupplierContractGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSubGroupUpdateCommand, SupplierContractSubGroup>();
    }
}

public class SupplierContractSubGroupUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractSubGroupUpdateCommand, SupplierContractSubGroup>
{
    public SupplierContractSubGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractSubGroup> Handle(SupplierContractSubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractSubGroups.SingleOrDefaultAsync(e => e.SupplierContractSubGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractSubGroupValidator : AbstractValidator<SupplierContractSubGroupUpdateCommand>
{
    public UpdateSupplierContractSubGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}