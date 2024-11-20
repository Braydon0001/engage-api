namespace Engage.Application.Services.TrainingFiles.Queries;

public class TrainingFileListQuery : IRequest<List<TrainingFileDto>>
{
    public int? TrainingId { get; set; }
}

public class TrainingFileListHandler : ListQueryHandler, IRequestHandler<TrainingFileListQuery, List<TrainingFileDto>>
{
    public TrainingFileListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<TrainingFileDto>> Handle(TrainingFileListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.TrainingFiles.IgnoreQueryFilters().AsQueryable().AsNoTracking();

        if (query.TrainingId.HasValue)
        {
            queryable = queryable.Where(e => e.TrainingId == query.TrainingId.Value);
        }

        return await queryable.OrderBy(e => e.TrainingFileId)
                              .ProjectTo<TrainingFileDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}