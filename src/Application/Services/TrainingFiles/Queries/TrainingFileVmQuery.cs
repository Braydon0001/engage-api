namespace Engage.Application.Services.TrainingFiles.Queries;

public class TrainingFileVmQuery : IRequest<TrainingFileVm>
{
    public int Id { get; set; }
}

public class TrainingFileVmHandler : VmQueryHandler, IRequestHandler<TrainingFileVmQuery, TrainingFileVm>
{
    public TrainingFileVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingFileVm> Handle(TrainingFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.TrainingFiles.IgnoreQueryFilters().AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Training)
                             .Include(e => e.TrainingFileType);

        var entity = await queryable.SingleOrDefaultAsync(e => e.TrainingFileId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<TrainingFileVm>(entity);
    }
}