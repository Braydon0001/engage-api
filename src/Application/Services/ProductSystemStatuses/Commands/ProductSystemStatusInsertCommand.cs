// auto-generated
namespace Engage.Application.Services.ProductSystemStatuses.Commands;

public class ProductSystemStatusInsertCommand : IMapTo<ProductSystemStatus>, IRequest<ProductSystemStatus>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSystemStatusInsertCommand, ProductSystemStatus>();
    }
}

public class ProductSystemStatusInsertHandler : InsertHandler, IRequestHandler<ProductSystemStatusInsertCommand, ProductSystemStatus>
{
    public ProductSystemStatusInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSystemStatus> Handle(ProductSystemStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductSystemStatusInsertCommand, ProductSystemStatus>(command);
        
        _context.ProductSystemStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductSystemStatusInsertValidator : AbstractValidator<ProductSystemStatusInsertCommand>
{
    public ProductSystemStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}