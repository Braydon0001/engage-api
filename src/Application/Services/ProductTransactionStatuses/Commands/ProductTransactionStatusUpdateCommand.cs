// auto-generated
namespace Engage.Application.Services.ProductTransactionStatuses.Commands;

public class ProductTransactionStatusUpdateCommand : IMapTo<ProductTransactionStatus>, IRequest<ProductTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionStatusUpdateCommand, ProductTransactionStatus>();
    }
}

public class ProductTransactionStatusUpdateHandler : UpdateHandler, IRequestHandler<ProductTransactionStatusUpdateCommand, ProductTransactionStatus>
{
    public ProductTransactionStatusUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransactionStatus> Handle(ProductTransactionStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductTransactionStatuses.SingleOrDefaultAsync(e => e.ProductTransactionStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductTransactionStatusValidator : AbstractValidator<ProductTransactionStatusUpdateCommand>
{
    public UpdateProductTransactionStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}