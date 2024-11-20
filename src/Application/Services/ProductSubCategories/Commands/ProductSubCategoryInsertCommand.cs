// auto-generated
namespace Engage.Application.Services.ProductSubCategories.Commands;

public class ProductSubCategoryInsertCommand : IMapTo<ProductSubCategory>, IRequest<ProductSubCategory>
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubCategoryInsertCommand, ProductSubCategory>();
    }
}

public class ProductSubCategoryInsertHandler : InsertHandler, IRequestHandler<ProductSubCategoryInsertCommand, ProductSubCategory>
{
    public ProductSubCategoryInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSubCategory> Handle(ProductSubCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductSubCategoryInsertCommand, ProductSubCategory>(command);
        
        _context.ProductSubCategories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductSubCategoryInsertValidator : AbstractValidator<ProductSubCategoryInsertCommand>
{
    public ProductSubCategoryInsertValidator()
    {
        RuleFor(e => e.ProductCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}