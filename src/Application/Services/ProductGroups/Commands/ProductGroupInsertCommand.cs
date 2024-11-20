// auto-generated
namespace Engage.Application.Services.ProductGroups.Commands;

public class ProductGroupInsertCommand : IMapTo<ProductGroup>, IRequest<ProductGroup>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductGroupInsertCommand, ProductGroup>();
    }
}

public class ProductGroupInsertHandler : InsertHandler, IRequestHandler<ProductGroupInsertCommand, ProductGroup>
{
    public ProductGroupInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductGroup> Handle(ProductGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductGroupInsertCommand, ProductGroup>(command);
        
        _context.ProductGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductGroupInsertValidator : AbstractValidator<ProductGroupInsertCommand>
{
    public ProductGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}