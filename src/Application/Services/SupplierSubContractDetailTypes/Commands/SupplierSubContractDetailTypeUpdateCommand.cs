namespace Engage.Application.Services.SupplierSubContractDetailTypes.Commands;

public class SupplierSubContractDetailTypeUpdateCommand : IMapTo<SupplierSubContractDetailType>, IRequest<SupplierSubContractDetailType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetailTypeUpdateCommand, SupplierSubContractDetailType>();
    }
}
public class SupplierSubContractDetailTypeUpdateHandler : InsertHandler, IRequestHandler<SupplierSubContractDetailTypeUpdateCommand, SupplierSubContractDetailType>
{
    public SupplierSubContractDetailTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractDetailType> Handle(SupplierSubContractDetailTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierSubContractDetailTypes.SingleOrDefaultAsync(e => e.SupplierSubContractDetailTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}
public class SupplierSubContractDetailTypeUpdateValidator : AbstractValidator<SupplierSubContractDetailTypeUpdateCommand>
{
    public SupplierSubContractDetailTypeUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}