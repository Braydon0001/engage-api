namespace Engage.Application.Services.TrainingFileTypes.Queries;

public class TrainingFileTypeVmQuery : IRequest<TrainingFileTypeVm>
{
    public int Id { get; set; }
}

public class TrainingFileTypeVmHandler : VmQueryHandler, IRequestHandler<TrainingFileTypeVmQuery, TrainingFileTypeVm>
{
    public TrainingFileTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingFileTypeVm> Handle(TrainingFileTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.TrainingFileTypes.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.TrainingFileTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<TrainingFileTypeVm>(entity);
    }
}