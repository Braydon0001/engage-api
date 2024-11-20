namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Queries;

public class EmployeeTransactionRemunerationTypeOptionQuery : IRequest<List<EmployeeTransactionRemunerationTypeOption>>
{ 

}

public record EmployeeTransactionRemunerationTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeTransactionRemunerationTypeOptionQuery, List<EmployeeTransactionRemunerationTypeOption>>
{
    public async Task<List<EmployeeTransactionRemunerationTypeOption>> Handle(EmployeeTransactionRemunerationTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeTransactionRemunerationTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeTransactionRemunerationTypeId)
                              .ProjectTo<EmployeeTransactionRemunerationTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}