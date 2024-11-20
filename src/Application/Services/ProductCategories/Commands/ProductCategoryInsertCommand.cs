// auto-generated
namespace Engage.Application.Services.ProductCategories.Commands;

public class ProductCategoryInsertCommand : IMapTo<ProductCategory>, IRequest<ProductCategory>
{
    public int ProductSubGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCategoryInsertCommand, ProductCategory>();
    }
}

public class ProductCategoryInsertHandler : InsertHandler, IRequestHandler<ProductCategoryInsertCommand, ProductCategory>
{
    public ProductCategoryInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductCategory> Handle(ProductCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductCategoryInsertCommand, ProductCategory>(command);
        
        _context.ProductCategories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductCategoryInsertValidator : AbstractValidator<ProductCategoryInsertCommand>
{
    public ProductCategoryInsertValidator()
    {
        RuleFor(e => e.ProductSubGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}