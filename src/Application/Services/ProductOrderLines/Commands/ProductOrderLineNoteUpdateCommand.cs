namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLineNoteUpdateCommand : IRequest<ProductOrderLine>
{
    public int Id { get; set; }
    public string Note { get; set; }
}
public record ProductOrderLineNoteUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineNoteUpdateCommand, ProductOrderLine>
{
    public async Task<ProductOrderLine> Handle(ProductOrderLineNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var orderLine = await Context.ProductOrderLines.FirstOrDefaultAsync(e => e.ProductOrderLineId == command.Id, cancellationToken);

        orderLine.Note = command.Note;

        await Context.SaveChangesAsync(cancellationToken);

        return orderLine;
    }
}
public class ProductOrderLineNoteUpdateValidator : AbstractValidator<ProductOrderLineNoteUpdateCommand>
{
    public ProductOrderLineNoteUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty();
    }
}