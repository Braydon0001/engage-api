namespace Engage.Application.Services.NPrintings.Queries;

public record NPrintingListQuery(int NPrintingBatchId) : IRequest<List<NPrintingDto>>
{
}

public class NPrintingListHandler : ListQueryHandler, IRequestHandler<NPrintingListQuery, List<NPrintingDto>>
{
    public NPrintingListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<NPrintingDto>> Handle(NPrintingListQuery query, CancellationToken cancellationToken)
    {
        // Left Join
        //https://learn.microsoft.com/en-us/ef/core/querying/complex-query-operators#left-join
        var queryable = from a in _context.NPrintings.Where(e => e.NPrintingBatchId == query.NPrintingBatchId).OrderBy(e => e.NPrintingId)
                        join b in _context.WebFiles
                        on a.NPrintingId equals b.NPrintingId into grouping
                        from b in grouping.DefaultIfEmpty()
                        select new NPrintingDto
                        {
                            Id = a.NPrintingId,
                            FileName = a.FileName,
                            ProcessedDate = a.ProcessedDate,
                            Error = a.Error,
                            Files = b.Files
                        };

        return await queryable.AsNoTracking().ToListAsync(cancellationToken);
    }
}
