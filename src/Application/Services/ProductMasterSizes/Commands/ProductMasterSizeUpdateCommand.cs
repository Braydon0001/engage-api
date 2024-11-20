// auto-generated
namespace Engage.Application.Services.ProductMasterSizes.Commands;

public class ProductMasterSizeUpdateCommand : IMapTo<ProductMasterSize>, IRequest<ProductMasterSize>
{
    public int Id { get; set; }
    public int ProductMasterId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSizeUpdateCommand, ProductMasterSize>();
    }
}

public class ProductMasterSizeUpdateHandler : UpdateHandler, IRequestHandler<ProductMasterSizeUpdateCommand, ProductMasterSize>
{
    public ProductMasterSizeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterSize> Handle(ProductMasterSizeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductMasterSizes.SingleOrDefaultAsync(e => e.ProductMasterSizeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductMasterSizeValidator : AbstractValidator<ProductMasterSizeUpdateCommand>
{
    public UpdateProductMasterSizeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductMasterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}