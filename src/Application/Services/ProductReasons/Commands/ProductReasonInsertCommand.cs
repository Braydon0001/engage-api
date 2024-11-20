// auto-generated
namespace Engage.Application.Services.ProductReasons.Commands;

public class ProductReasonInsertCommand : IMapTo<ProductReason>, IRequest<ProductReason>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductReasonInsertCommand, ProductReason>();
    }
}

public class ProductReasonInsertHandler : InsertHandler, IRequestHandler<ProductReasonInsertCommand, ProductReason>
{
    public ProductReasonInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductReason> Handle(ProductReasonInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductReasonInsertCommand, ProductReason>(command);
        
        _context.ProductReasons.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductReasonInsertValidator : AbstractValidator<ProductReasonInsertCommand>
{
    public ProductReasonInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}