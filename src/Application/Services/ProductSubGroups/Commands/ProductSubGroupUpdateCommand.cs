// auto-generated
namespace Engage.Application.Services.ProductSubGroups.Commands;

public class ProductSubGroupUpdateCommand : IMapTo<ProductSubGroup>, IRequest<ProductSubGroup>
{
    public int Id { get; set; }
    public int ProductGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubGroupUpdateCommand, ProductSubGroup>();
    }
}

public class ProductSubGroupUpdateHandler : UpdateHandler, IRequestHandler<ProductSubGroupUpdateCommand, ProductSubGroup>
{
    public ProductSubGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSubGroup> Handle(ProductSubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductSubGroups.SingleOrDefaultAsync(e => e.ProductSubGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductSubGroupValidator : AbstractValidator<ProductSubGroupUpdateCommand>
{
    public UpdateProductSubGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}