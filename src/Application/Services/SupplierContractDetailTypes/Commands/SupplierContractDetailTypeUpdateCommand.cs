// auto-generated
namespace Engage.Application.Services.SupplierContractDetailTypes.Commands;

public class SupplierContractDetailTypeUpdateCommand : IMapTo<SupplierContractDetailType>, IRequest<SupplierContractDetailType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetailTypeUpdateCommand, SupplierContractDetailType>();
    }
}

public class SupplierContractDetailTypeUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractDetailTypeUpdateCommand, SupplierContractDetailType>
{
    public SupplierContractDetailTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractDetailType> Handle(SupplierContractDetailTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractDetailTypes.SingleOrDefaultAsync(e => e.SupplierContractDetailTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractDetailTypeValidator : AbstractValidator<SupplierContractDetailTypeUpdateCommand>
{
    public UpdateSupplierContractDetailTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}