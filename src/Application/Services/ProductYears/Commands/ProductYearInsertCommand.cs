// auto-generated
namespace Engage.Application.Services.ProductYears.Commands;

public class ProductYearInsertCommand : IMapTo<ProductYear>, IRequest<ProductYear>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductYearInsertCommand, ProductYear>();
    }
}

public class ProductYearInsertHandler : InsertHandler, IRequestHandler<ProductYearInsertCommand, ProductYear>
{
    public ProductYearInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductYear> Handle(ProductYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductYearInsertCommand, ProductYear>(command);
        
        _context.ProductYears.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductYearInsertValidator : AbstractValidator<ProductYearInsertCommand>
{
    public ProductYearInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}