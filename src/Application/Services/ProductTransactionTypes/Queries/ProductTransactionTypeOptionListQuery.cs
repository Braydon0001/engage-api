// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Queries;

public class ProductTransactionTypeOptionListQuery : IRequest<List<ProductTransactionTypeOption>>
{
    public bool ReturnAll { get; set; } = false;
}

public class ProductTransactionTypeOptionListHandler : ListQueryHandler, IRequestHandler<ProductTransactionTypeOptionListQuery, List<ProductTransactionTypeOption>>
{
    public ProductTransactionTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductTransactionTypeOption>> Handle(ProductTransactionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactionTypes.AsQueryable().AsNoTracking();

        var options = await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductTransactionTypeOption>(_mapper.ConfigurationProvider)
                              .OrderBy(e => e.Id)
                              .ToListAsync(cancellationToken);

        if (query.ReturnAll)
        {
            return options;
        }

        var newList = options.Where(e => e.Id != 3 && e.Id != 4).ToList();

        List<ProductTransactionTypeOption> optionList = new List<ProductTransactionTypeOption>();
        optionList.AddRange(newList);
        optionList.Add(new ProductTransactionTypeOption() { Id = 3, Name = "Transfer" });

        return optionList.OrderBy(e => e.Id).ToList();
    }
}