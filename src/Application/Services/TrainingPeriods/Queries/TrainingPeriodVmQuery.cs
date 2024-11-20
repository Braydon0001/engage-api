using Engage.Application.Services.TrainingPeriods.Models;

namespace Engage.Application.Services.TrainingPeriods.Queries;

public class TrainingPeriodVmQuery : GetByIdQuery, IRequest<TrainingPeriodVm>
{
}

public class GetTrainingPeriodVmQueryHandler : BaseQueryHandler, IRequestHandler<TrainingPeriodVmQuery, TrainingPeriodVm>
{
    public GetTrainingPeriodVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<TrainingPeriodVm> Handle(TrainingPeriodVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingPeriods.Include(e => e.TrainingYear)
                                                .SingleOrDefaultAsync(x => x.TrainingPeriodId == request.Id, cancellationToken);

        return _mapper.Map<TrainingPeriod, TrainingPeriodVm>(entity);
    }
}
