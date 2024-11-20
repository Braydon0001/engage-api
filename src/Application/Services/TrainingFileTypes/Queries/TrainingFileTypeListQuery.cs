namespace Engage.Application.Services.TrainingFileTypes.Queries;

public class TrainingFileTypeListQuery : IRequest<List<TrainingFileTypeDto>>
{

}

public class TrainingFileTypeListHandler : ListQueryHandler, IRequestHandler<TrainingFileTypeListQuery, List<TrainingFileTypeDto>>
{
    public TrainingFileTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<TrainingFileTypeDto>> Handle(TrainingFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.TrainingFileTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<TrainingFileTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}