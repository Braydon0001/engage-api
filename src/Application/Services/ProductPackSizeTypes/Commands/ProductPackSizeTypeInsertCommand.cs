// auto-generated
namespace Engage.Application.Services.ProductPackSizeTypes.Commands;

public class ProductPackSizeTypeInsertCommand : IMapTo<ProductPackSizeType>, IRequest<ProductPackSizeType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPackSizeTypeInsertCommand, ProductPackSizeType>();
    }
}

public class ProductPackSizeTypeInsertHandler : InsertHandler, IRequestHandler<ProductPackSizeTypeInsertCommand, ProductPackSizeType>
{
    public ProductPackSizeTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductPackSizeType> Handle(ProductPackSizeTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductPackSizeTypeInsertCommand, ProductPackSizeType>(command);
        
        _context.ProductPackSizeTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductPackSizeTypeInsertValidator : AbstractValidator<ProductPackSizeTypeInsertCommand>
{
    public ProductPackSizeTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}