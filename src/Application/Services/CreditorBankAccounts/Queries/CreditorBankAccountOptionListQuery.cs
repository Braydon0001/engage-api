// auto-generated
namespace Engage.Application.Services.CreditorBankAccounts.Queries;

public class CreditorBankAccountOptionListQuery : IRequest<List<CreditorBankAccountOption>>
{ 

}

public class CreditorBankAccountOptionListHandler : ListQueryHandler, IRequestHandler<CreditorBankAccountOptionListQuery, List<CreditorBankAccountOption>>
{
    public CreditorBankAccountOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<CreditorBankAccountOption>> Handle(CreditorBankAccountOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.CreditorBankAccounts.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CreditorBankAccountOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}