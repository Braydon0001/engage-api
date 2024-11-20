namespace Engage.Application.Services.ExpenseTypes.Queries;

public class ExpenseTypeListQuery : IRequest<List<ExpenseTypeDto>>
{

}

public record ExpenseTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExpenseTypeListQuery, List<ExpenseTypeDto>>
{
    public async Task<List<ExpenseTypeDto>> Handle(ExpenseTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ExpenseTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ExpenseTypeId)
                              .ProjectTo<ExpenseTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}