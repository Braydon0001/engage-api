// auto-generated
namespace Engage.Application.Services.ProductYears.Commands;

public class ProductYearUpdateCommand : IMapTo<ProductYear>, IRequest<ProductYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductYearUpdateCommand, ProductYear>();
    }
}

public class ProductYearUpdateHandler : UpdateHandler, IRequestHandler<ProductYearUpdateCommand, ProductYear>
{
    public ProductYearUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductYear> Handle(ProductYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductYears.SingleOrDefaultAsync(e => e.ProductYearId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductYearValidator : AbstractValidator<ProductYearUpdateCommand>
{
    public UpdateProductYearValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}