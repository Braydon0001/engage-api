using Engage.Application.Services.StoreBankDetails.Models;

namespace Engage.Application.Services.StoreBankDetails.Queries;

public class StoreBankDetailsQuery
    : GetQuery, IRequest<ListResult<StoreBankDetailDto>>
{
    public int StoreId { get; set; }
}

public class StoreBankDetailsQueryHandler : BaseQueryHandler, IRequestHandler<StoreBankDetailsQuery, ListResult<StoreBankDetailDto>>
{
    public StoreBankDetailsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreBankDetailDto>> Handle(StoreBankDetailsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.StoreBankDetails.Where(e => e.StoreId == request.StoreId)
                                                      .OrderBy(e => e.Bank)
                                                      .ThenBy(e => e.AccountNumber)
                                                      .ProjectTo<StoreBankDetailDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

        return new ListResult<StoreBankDetailDto>(entities);
    }
}
