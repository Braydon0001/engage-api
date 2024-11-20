namespace Engage.Application.Services.Creditors.Queries;

public class CreditorsQuery : IRequest<ListResult<CreditorDto>>
{
    public int? CreditorStatusId { get; set; }
    public List<int> CreditorStatusIds { get; set; }
}

public class CreditorsQueryHandler : BaseQueryHandler, IRequestHandler<CreditorsQuery, ListResult<CreditorDto>>
{
    public CreditorsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<CreditorDto>> Handle(CreditorsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Creditors.AsQueryable();

        if (request.CreditorStatusIds != null && request.CreditorStatusIds.Count > 0)
        {
            queryable = queryable.Where(e => request.CreditorStatusIds.Contains(e.CreditorStatusId));
        }

        if (request.CreditorStatusId.HasValue) { queryable = queryable.Where(e => e.CreditorStatusId == request.CreditorStatusId.Value); }

        var entities = await queryable.OrderByDescending(e => e.CreditorId)
                                      .ProjectTo<CreditorDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<CreditorDto>(entities);
    }
}
