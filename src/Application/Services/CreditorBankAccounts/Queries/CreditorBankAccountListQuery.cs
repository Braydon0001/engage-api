// auto-generated
namespace Engage.Application.Services.CreditorBankAccounts.Queries;

public class CreditorBankAccountListQuery : IRequest<List<CreditorBankAccountDto>>
{

}

public class CreditorBankAccountListHandler : ListQueryHandler, IRequestHandler<CreditorBankAccountListQuery, List<CreditorBankAccountDto>>
{
    public CreditorBankAccountListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<CreditorBankAccountDto>> Handle(CreditorBankAccountListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.CreditorBankAccounts.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CreditorBankAccountDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}