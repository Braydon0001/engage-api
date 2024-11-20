using Engage.Application.Services.TrainingPeriods.Models;

namespace Engage.Application.Services.TrainingPeriods.Queries;

public class TrainingPeriodsQuery : GetQuery, IRequest<ListResult<TrainingPeriodDto>>
{
    public int? TrainingYearId { get; set; }
}

public class GetTrainingPeriodsQueryHandler : BaseQueryHandler, IRequestHandler<TrainingPeriodsQuery, ListResult<TrainingPeriodDto>>
{
    public GetTrainingPeriodsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<TrainingPeriodDto>> Handle(TrainingPeriodsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TrainingPeriods.AsQueryable().AsNoTracking();

        if (request.TrainingYearId.HasValue)
        {
            query = query.Where(e => e.TrainingYearId == request.TrainingYearId);
        }

        var entities = await query.OrderBy(e => e.TrainingPeriodId)
                                  .ProjectTo<TrainingPeriodDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        return new ListResult<TrainingPeriodDto>(entities);
    }
}
