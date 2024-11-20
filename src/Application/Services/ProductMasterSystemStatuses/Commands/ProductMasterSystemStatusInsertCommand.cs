// auto-generated
namespace Engage.Application.Services.ProductMasterSystemStatuses.Commands;

public class ProductMasterSystemStatusInsertCommand : IMapTo<ProductMasterSystemStatus>, IRequest<ProductMasterSystemStatus>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSystemStatusInsertCommand, ProductMasterSystemStatus>();
    }
}

public class ProductMasterSystemStatusInsertHandler : InsertHandler, IRequestHandler<ProductMasterSystemStatusInsertCommand, ProductMasterSystemStatus>
{
    public ProductMasterSystemStatusInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterSystemStatus> Handle(ProductMasterSystemStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductMasterSystemStatusInsertCommand, ProductMasterSystemStatus>(command);
        
        _context.ProductMasterSystemStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductMasterSystemStatusInsertValidator : AbstractValidator<ProductMasterSystemStatusInsertCommand>
{
    public ProductMasterSystemStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}