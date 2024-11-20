namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLineDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}
public record ProductOrderLineDeleteHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProductOrderLineDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderLines.Where(e => e.ProductOrderLineId == command.Id)
                                                    .FirstOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            return null;
        }

        Context.ProductOrderLines.Remove(entity);

        return await Context.SaveChangesAsync(cancellationToken);
    }
}