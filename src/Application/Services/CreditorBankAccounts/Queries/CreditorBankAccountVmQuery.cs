// auto-generated
namespace Engage.Application.Services.CreditorBankAccounts.Queries;

public class CreditorBankAccountVmQuery : IRequest<CreditorBankAccountVm>
{
    public int Id { get; set; }
}

public class CreditorBankAccountVmHandler : VmQueryHandler, IRequestHandler<CreditorBankAccountVmQuery, CreditorBankAccountVm>
{
    public CreditorBankAccountVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<CreditorBankAccountVm> Handle(CreditorBankAccountVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.CreditorBankAccounts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.BankName)
                             .Include(e => e.BankAccountType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CreditorBankAccountId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<CreditorBankAccountVm>(entity);
    }
}