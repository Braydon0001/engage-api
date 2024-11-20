// auto-generated
namespace Engage.Application.Services.SupplierSubContractTypes.Commands;

public class SupplierSubContractTypeUpdateCommand : IMapTo<SupplierSubContractType>, IRequest<SupplierSubContractType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractTypeUpdateCommand, SupplierSubContractType>();
    }
}

public class SupplierSubContractTypeUpdateHandler : UpdateHandler, IRequestHandler<SupplierSubContractTypeUpdateCommand, SupplierSubContractType>
{
    public SupplierSubContractTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractType> Handle(SupplierSubContractTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierSubContractTypes.SingleOrDefaultAsync(e => e.SupplierSubContractTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierSubContractTypeValidator : AbstractValidator<SupplierSubContractTypeUpdateCommand>
{
    public UpdateSupplierSubContractTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}