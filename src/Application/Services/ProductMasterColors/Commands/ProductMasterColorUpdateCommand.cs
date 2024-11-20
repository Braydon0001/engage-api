// auto-generated
namespace Engage.Application.Services.ProductMasterColors.Commands;

public class ProductMasterColorUpdateCommand : IMapTo<ProductMasterColor>, IRequest<ProductMasterColor>
{
    public int Id { get; set; }
    public int ProductMasterId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterColorUpdateCommand, ProductMasterColor>();
    }
}

public class ProductMasterColorUpdateHandler : UpdateHandler, IRequestHandler<ProductMasterColorUpdateCommand, ProductMasterColor>
{
    public ProductMasterColorUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterColor> Handle(ProductMasterColorUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductMasterColors.SingleOrDefaultAsync(e => e.ProductMasterColorId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductMasterColorValidator : AbstractValidator<ProductMasterColorUpdateCommand>
{
    public UpdateProductMasterColorValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductMasterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}