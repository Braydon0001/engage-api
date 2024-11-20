namespace Engage.Application.Services.ProductOrderLineStatuses.Commands;

public class ProductOrderLineStatusUpdateCommand : IMapTo<ProductOrderLineStatus>, IRequest<ProductOrderLineStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineStatusUpdateCommand, ProductOrderLineStatus>();
    }
}

public record ProductOrderLineStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineStatusUpdateCommand, ProductOrderLineStatus>
{
    public async Task<ProductOrderLineStatus> Handle(ProductOrderLineStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderLineStatuses.SingleOrDefaultAsync(e => e.ProductOrderLineStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductOrderLineStatusValidator : AbstractValidator<ProductOrderLineStatusUpdateCommand>
{
    public UpdateProductOrderLineStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}