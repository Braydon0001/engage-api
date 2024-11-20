// auto-generated
namespace Engage.Application.Services.ProductSizeTypes.Commands;

public class ProductSizeTypeInsertCommand : IMapTo<ProductSizeType>, IRequest<ProductSizeType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSizeTypeInsertCommand, ProductSizeType>();
    }
}

public class ProductSizeTypeInsertHandler : InsertHandler, IRequestHandler<ProductSizeTypeInsertCommand, ProductSizeType>
{
    public ProductSizeTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSizeType> Handle(ProductSizeTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductSizeTypeInsertCommand, ProductSizeType>(command);
        
        _context.ProductSizeTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductSizeTypeInsertValidator : AbstractValidator<ProductSizeTypeInsertCommand>
{
    public ProductSizeTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}