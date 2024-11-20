// auto-generated
namespace Engage.Application.Services.ProductMasterSystemStatuses.Commands;

public class ProductMasterSystemStatusUpdateCommand : IMapTo<ProductMasterSystemStatus>, IRequest<ProductMasterSystemStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSystemStatusUpdateCommand, ProductMasterSystemStatus>();
    }
}

public class ProductMasterSystemStatusUpdateHandler : UpdateHandler, IRequestHandler<ProductMasterSystemStatusUpdateCommand, ProductMasterSystemStatus>
{
    public ProductMasterSystemStatusUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterSystemStatus> Handle(ProductMasterSystemStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductMasterSystemStatuses.SingleOrDefaultAsync(e => e.ProductMasterSystemStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductMasterSystemStatusValidator : AbstractValidator<ProductMasterSystemStatusUpdateCommand>
{
    public UpdateProductMasterSystemStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}