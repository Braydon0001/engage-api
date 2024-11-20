// auto-generated
namespace Engage.Application.Services.ProductMasterStatuses.Commands;

public class ProductMasterStatusInsertCommand : IMapTo<ProductMasterStatus>, IRequest<ProductMasterStatus>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterStatusInsertCommand, ProductMasterStatus>();
    }
}

public class ProductMasterStatusInsertHandler : InsertHandler, IRequestHandler<ProductMasterStatusInsertCommand, ProductMasterStatus>
{
    public ProductMasterStatusInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterStatus> Handle(ProductMasterStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductMasterStatusInsertCommand, ProductMasterStatus>(command);
        
        _context.ProductMasterStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductMasterStatusInsertValidator : AbstractValidator<ProductMasterStatusInsertCommand>
{
    public ProductMasterStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}