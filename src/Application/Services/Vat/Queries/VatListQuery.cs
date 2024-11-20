using Engage.Application.Services.Vat.Models;

namespace Engage.Application.Services.Vat.Queries;

public class VatListQuery : GetQuery, IRequest<ListResult<VatDto>>
{
}

public class VatListQueryHandler : BaseQueryHandler, IRequestHandler<VatListQuery, ListResult<VatDto>>
{
    public VatListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<VatDto>> Handle(VatListQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.Vat.OrderBy(e => e.VatId)
                                         .ProjectTo<VatDto>(_mapper.ConfigurationProvider)
                                         .ToListAsync(cancellationToken);

        return new ListResult<VatDto>(entities);
    }
}
