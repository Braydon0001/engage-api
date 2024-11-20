namespace Engage.Application.Services.AuditEntryProperties.Queries;

public class AuditEntryPropertyListQuery : IRequest<List<AuditEntryPropertyDto>>
{
    public int AuditEntryId { get; init; }
}

public record AuditEntryPropertyListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AuditEntryPropertyListQuery, List<AuditEntryPropertyDto>>
{
    public async Task<List<AuditEntryPropertyDto>> Handle(AuditEntryPropertyListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.AuditEntryProperties.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.AuditEntryID == query.AuditEntryId);

        return await queryable.OrderBy(e => e.AuditEntryID)
                              .ThenBy(e => e.PropertyName)
                              .ProjectTo<AuditEntryPropertyDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}