// auto-generated
namespace Engage.Application.Services.ProductCategories.Commands;

public class ProductCategoryUpdateCommand : IMapTo<ProductCategory>, IRequest<ProductCategory>
{
    public int Id { get; set; }
    public int ProductSubGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCategoryUpdateCommand, ProductCategory>();
    }
}

public class ProductCategoryUpdateHandler : UpdateHandler, IRequestHandler<ProductCategoryUpdateCommand, ProductCategory>
{
    public ProductCategoryUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductCategory> Handle(ProductCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductCategories.SingleOrDefaultAsync(e => e.ProductCategoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductCategoryValidator : AbstractValidator<ProductCategoryUpdateCommand>
{
    public UpdateProductCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductSubGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}