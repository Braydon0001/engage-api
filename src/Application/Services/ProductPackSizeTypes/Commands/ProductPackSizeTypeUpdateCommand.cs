// auto-generated
namespace Engage.Application.Services.ProductPackSizeTypes.Commands;

public class ProductPackSizeTypeUpdateCommand : IMapTo<ProductPackSizeType>, IRequest<ProductPackSizeType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPackSizeTypeUpdateCommand, ProductPackSizeType>();
    }
}

public class ProductPackSizeTypeUpdateHandler : UpdateHandler, IRequestHandler<ProductPackSizeTypeUpdateCommand, ProductPackSizeType>
{
    public ProductPackSizeTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductPackSizeType> Handle(ProductPackSizeTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductPackSizeTypes.SingleOrDefaultAsync(e => e.ProductPackSizeTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductPackSizeTypeValidator : AbstractValidator<ProductPackSizeTypeUpdateCommand>
{
    public UpdateProductPackSizeTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}