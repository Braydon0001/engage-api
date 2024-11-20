namespace Engage.Application.Services.TrainingDurations.Queries;

public class TrainingDurationListQuery : IRequest<List<TrainingDurationDto>>
{
}

public record TrainingDurationListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<TrainingDurationListQuery, List<TrainingDurationDto>>
{
    public async Task<List<TrainingDurationDto>> Handle(TrainingDurationListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.TrainingDurations.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.TrainingDurationId)
                              .ProjectTo<TrainingDurationDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
