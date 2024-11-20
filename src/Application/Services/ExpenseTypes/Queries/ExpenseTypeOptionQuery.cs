namespace Engage.Application.Services.ExpenseTypes.Queries;

public class ExpenseTypeOptionQuery : IRequest<List<ExpenseTypeOption>>
{ 

}

public record ExpenseTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExpenseTypeOptionQuery, List<ExpenseTypeOption>>
{
    public async Task<List<ExpenseTypeOption>> Handle(ExpenseTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ExpenseTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ExpenseTypeId)
                              .ProjectTo<ExpenseTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}