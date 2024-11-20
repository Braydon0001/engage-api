// auto-generated
namespace Engage.Application.Services.ProductSubGroups.Commands;

public class ProductSubGroupInsertCommand : IMapTo<ProductSubGroup>, IRequest<ProductSubGroup>
{
    public int ProductGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubGroupInsertCommand, ProductSubGroup>();
    }
}

public class ProductSubGroupInsertHandler : InsertHandler, IRequestHandler<ProductSubGroupInsertCommand, ProductSubGroup>
{
    public ProductSubGroupInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSubGroup> Handle(ProductSubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductSubGroupInsertCommand, ProductSubGroup>(command);
        
        _context.ProductSubGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductSubGroupInsertValidator : AbstractValidator<ProductSubGroupInsertCommand>
{
    public ProductSubGroupInsertValidator()
    {
        RuleFor(e => e.ProductGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}