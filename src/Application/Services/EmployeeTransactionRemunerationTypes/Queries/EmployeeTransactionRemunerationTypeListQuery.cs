namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Queries;

public class EmployeeTransactionRemunerationTypeListQuery : IRequest<List<EmployeeTransactionRemunerationTypeDto>>
{

}

public record EmployeeTransactionRemunerationTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeTransactionRemunerationTypeListQuery, List<EmployeeTransactionRemunerationTypeDto>>
{
    public async Task<List<EmployeeTransactionRemunerationTypeDto>> Handle(EmployeeTransactionRemunerationTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeTransactionRemunerationTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeTransactionRemunerationTypeId)
                              .ProjectTo<EmployeeTransactionRemunerationTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}