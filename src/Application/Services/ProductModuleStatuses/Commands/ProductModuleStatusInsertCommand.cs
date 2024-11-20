// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Commands;

public class ProductModuleStatusInsertCommand : IMapTo<ProductModuleStatus>, IRequest<ProductModuleStatus>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductModuleStatusInsertCommand, ProductModuleStatus>();
    }
}

public class ProductModuleStatusInsertHandler : InsertHandler, IRequestHandler<ProductModuleStatusInsertCommand, ProductModuleStatus>
{
    public ProductModuleStatusInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductModuleStatus> Handle(ProductModuleStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductModuleStatusInsertCommand, ProductModuleStatus>(command);
        
        _context.ProductModuleStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductModuleStatusInsertValidator : AbstractValidator<ProductModuleStatusInsertCommand>
{
    public ProductModuleStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}