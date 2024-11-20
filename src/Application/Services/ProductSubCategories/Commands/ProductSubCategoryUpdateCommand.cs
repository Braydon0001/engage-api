// auto-generated
namespace Engage.Application.Services.ProductSubCategories.Commands;

public class ProductSubCategoryUpdateCommand : IMapTo<ProductSubCategory>, IRequest<ProductSubCategory>
{
    public int Id { get; set; }
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubCategoryUpdateCommand, ProductSubCategory>();
    }
}

public class ProductSubCategoryUpdateHandler : UpdateHandler, IRequestHandler<ProductSubCategoryUpdateCommand, ProductSubCategory>
{
    public ProductSubCategoryUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSubCategory> Handle(ProductSubCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductSubCategories.SingleOrDefaultAsync(e => e.ProductSubCategoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductSubCategoryValidator : AbstractValidator<ProductSubCategoryUpdateCommand>
{
    public UpdateProductSubCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}