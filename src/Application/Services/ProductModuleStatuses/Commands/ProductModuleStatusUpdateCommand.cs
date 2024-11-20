// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Commands;

public class ProductModuleStatusUpdateCommand : IMapTo<ProductModuleStatus>, IRequest<ProductModuleStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductModuleStatusUpdateCommand, ProductModuleStatus>();
    }
}

public class ProductModuleStatusUpdateHandler : UpdateHandler, IRequestHandler<ProductModuleStatusUpdateCommand, ProductModuleStatus>
{
    public ProductModuleStatusUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductModuleStatus> Handle(ProductModuleStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductModuleStatuses.SingleOrDefaultAsync(e => e.ProductModuleStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductModuleStatusValidator : AbstractValidator<ProductModuleStatusUpdateCommand>
{
    public UpdateProductModuleStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}