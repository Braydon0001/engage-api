namespace Engage.Application.Services.TrainingFileTypes.Queries;

public class TrainingFileTypeOptionListQuery : IRequest<List<TrainingFileTypeOption>>
{

}

public class TrainingFileTypeOptionListHandler : ListQueryHandler, IRequestHandler<TrainingFileTypeOptionListQuery, List<TrainingFileTypeOption>>
{
    public TrainingFileTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<TrainingFileTypeOption>> Handle(TrainingFileTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.TrainingFileTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<TrainingFileTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}