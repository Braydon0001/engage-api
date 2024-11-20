// auto-generated
namespace Engage.Application.Services.ProductWarehouses.Commands;

public class ProductWarehouseUpdateCommand : IMapTo<ProductWarehouse>, IRequest<ProductWarehouse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public List<int> ProductWarehouseRegionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouseUpdateCommand, ProductWarehouse>();
    }
}

public class ProductWarehouseUpdateHandler : UpdateHandler, IRequestHandler<ProductWarehouseUpdateCommand, ProductWarehouse>
{
    private readonly IMediator _mediator;
    public ProductWarehouseUpdateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ProductWarehouse> Handle(ProductWarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductWarehouses.SingleOrDefaultAsync(e => e.ProductWarehouseId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        if (command.ProductWarehouseRegionIds != null)
        {
            await _mediator.Send(new ProductWarehouseUpdateRegionsCommand
            {
                EngageRegionIds = command.ProductWarehouseRegionIds,
                ProductWarehouseId = mappedEntity.ProductWarehouseId
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductWarehouseValidator : AbstractValidator<ProductWarehouseUpdateCommand>
{
    public UpdateProductWarehouseValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}