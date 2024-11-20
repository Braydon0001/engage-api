// auto-generated
namespace Engage.Application.Services.ProductSystemStatuses.Commands;

public class ProductSystemStatusUpdateCommand : IMapTo<ProductSystemStatus>, IRequest<ProductSystemStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSystemStatusUpdateCommand, ProductSystemStatus>();
    }
}

public class ProductSystemStatusUpdateHandler : UpdateHandler, IRequestHandler<ProductSystemStatusUpdateCommand, ProductSystemStatus>
{
    public ProductSystemStatusUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSystemStatus> Handle(ProductSystemStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductSystemStatuses.SingleOrDefaultAsync(e => e.ProductSystemStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductSystemStatusValidator : AbstractValidator<ProductSystemStatusUpdateCommand>
{
    public UpdateProductSystemStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}