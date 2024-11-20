// auto-generated
namespace Engage.Application.Services.ProductReasons.Commands;

public class ProductReasonUpdateCommand : IMapTo<ProductReason>, IRequest<ProductReason>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductReasonUpdateCommand, ProductReason>();
    }
}

public class ProductReasonUpdateHandler : UpdateHandler, IRequestHandler<ProductReasonUpdateCommand, ProductReason>
{
    public ProductReasonUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductReason> Handle(ProductReasonUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductReasons.SingleOrDefaultAsync(e => e.ProductReasonId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductReasonValidator : AbstractValidator<ProductReasonUpdateCommand>
{
    public UpdateProductReasonValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}