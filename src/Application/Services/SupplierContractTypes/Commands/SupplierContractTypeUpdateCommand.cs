// auto-generated
namespace Engage.Application.Services.SupplierContractTypes.Commands;

public class SupplierContractTypeUpdateCommand : IMapTo<SupplierContractType>, IRequest<SupplierContractType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractTypeUpdateCommand, SupplierContractType>();
    }
}

public class SupplierContractTypeUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractTypeUpdateCommand, SupplierContractType>
{
    public SupplierContractTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractType> Handle(SupplierContractTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractTypes.SingleOrDefaultAsync(e => e.SupplierContractTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractTypeValidator : AbstractValidator<SupplierContractTypeUpdateCommand>
{
    public UpdateSupplierContractTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}