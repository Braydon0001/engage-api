// auto-generated
namespace Engage.Application.Services.ProductGroups.Commands;

public class ProductGroupUpdateCommand : IMapTo<ProductGroup>, IRequest<ProductGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductGroupUpdateCommand, ProductGroup>();
    }
}

public class ProductGroupUpdateHandler : UpdateHandler, IRequestHandler<ProductGroupUpdateCommand, ProductGroup>
{
    public ProductGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductGroup> Handle(ProductGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductGroups.SingleOrDefaultAsync(e => e.ProductGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductGroupValidator : AbstractValidator<ProductGroupUpdateCommand>
{
    public UpdateProductGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}