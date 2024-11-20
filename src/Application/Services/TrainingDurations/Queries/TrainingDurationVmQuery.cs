namespace Engage.Application.Services.TrainingDurations.Queries;

public record TrainingDurationVmQuery(int Id) : IRequest<TrainingDurationVm>;

public record TrainingDurationVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<TrainingDurationVmQuery, TrainingDurationVm>
{
    public async Task<TrainingDurationVm> Handle(TrainingDurationVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.TrainingDurations.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.TrainingDurationId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<TrainingDurationVm>(entity);
    }
}