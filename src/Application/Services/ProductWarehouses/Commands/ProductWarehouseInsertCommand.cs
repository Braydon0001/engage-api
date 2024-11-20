// auto-generated
namespace Engage.Application.Services.ProductWarehouses.Commands;

public class ProductWarehouseInsertCommand : IMapTo<ProductWarehouse>, IRequest<ProductWarehouse>
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public List<int> ProductWarehouseRegionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouseInsertCommand, ProductWarehouse>();
    }
}

public class ProductWarehouseInsertHandler : InsertHandler, IRequestHandler<ProductWarehouseInsertCommand, ProductWarehouse>
{
    private readonly IMediator _mediator;
    public ProductWarehouseInsertHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ProductWarehouse> Handle(ProductWarehouseInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductWarehouseInsertCommand, ProductWarehouse>(command);

        _context.ProductWarehouses.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            if (command.ProductWarehouseRegionIds != null)
            {
                await _mediator.Send(new ProductWarehouseUpdateRegionsCommand
                {
                    EngageRegionIds = command.ProductWarehouseRegionIds,
                    ProductWarehouseId = entity.ProductWarehouseId
                }, cancellationToken);
            }
        }

        return entity;
    }
}

public class ProductWarehouseInsertValidator : AbstractValidator<ProductWarehouseInsertCommand>
{
    public ProductWarehouseInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}