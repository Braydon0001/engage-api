// auto-generated
namespace Engage.Application.Services.ProductMasterStatuses.Commands;

public class ProductMasterStatusUpdateCommand : IMapTo<ProductMasterStatus>, IRequest<ProductMasterStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterStatusUpdateCommand, ProductMasterStatus>();
    }
}

public class ProductMasterStatusUpdateHandler : UpdateHandler, IRequestHandler<ProductMasterStatusUpdateCommand, ProductMasterStatus>
{
    public ProductMasterStatusUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterStatus> Handle(ProductMasterStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductMasterStatuses.SingleOrDefaultAsync(e => e.ProductMasterStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductMasterStatusValidator : AbstractValidator<ProductMasterStatusUpdateCommand>
{
    public UpdateProductMasterStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}