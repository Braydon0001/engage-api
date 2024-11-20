// auto-generated
namespace Engage.Application.Services.SupplierContractSubGroups.Commands;

public class SupplierContractSubGroupInsertCommand : IMapTo<SupplierContractSubGroup>, IRequest<SupplierContractSubGroup>
{
    public int SupplierContractGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractSubGroupInsertCommand, SupplierContractSubGroup>();
    }
}

public class SupplierContractSubGroupInsertHandler : InsertHandler, IRequestHandler<SupplierContractSubGroupInsertCommand, SupplierContractSubGroup>
{
    public SupplierContractSubGroupInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractSubGroup> Handle(SupplierContractSubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierContractSubGroupInsertCommand, SupplierContractSubGroup>(command);
        
        _context.SupplierContractSubGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractSubGroupInsertValidator : AbstractValidator<SupplierContractSubGroupInsertCommand>
{
    public SupplierContractSubGroupInsertValidator()
    {
        RuleFor(e => e.SupplierContractGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}