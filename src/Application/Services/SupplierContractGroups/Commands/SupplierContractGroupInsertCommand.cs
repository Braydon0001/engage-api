// auto-generated
namespace Engage.Application.Services.SupplierContractGroups.Commands;

public class SupplierContractGroupInsertCommand : IMapTo<SupplierContractGroup>, IRequest<SupplierContractGroup>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractGroupInsertCommand, SupplierContractGroup>();
    }
}

public class SupplierContractGroupInsertHandler : InsertHandler, IRequestHandler<SupplierContractGroupInsertCommand, SupplierContractGroup>
{
    public SupplierContractGroupInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractGroup> Handle(SupplierContractGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierContractGroupInsertCommand, SupplierContractGroup>(command);
        
        _context.SupplierContractGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractGroupInsertValidator : AbstractValidator<SupplierContractGroupInsertCommand>
{
    public SupplierContractGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}