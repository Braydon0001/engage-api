namespace Engage.Application.Services.TrainingDurations.Queries;

public class TrainingDurationOptionQuery : IRequest<List<TrainingDurationOption>>
{
}

public record TrainingDurationOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<TrainingDurationOptionQuery, List<TrainingDurationOption>>
{
    public async Task<List<TrainingDurationOption>> Handle(TrainingDurationOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.TrainingDurations.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.TrainingDurationId)
                              .ProjectTo<TrainingDurationOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
