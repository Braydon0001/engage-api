using Engage.Application.Services.TrainingYears.Models;

namespace Engage.Application.Services.TrainingYears.Queries;

public class TrainingYearVmQuery : GetByIdQuery, IRequest<TrainingYearVm>
{
}

public class GetTrainingYearVmQueryHandler : BaseQueryHandler, IRequestHandler<TrainingYearVmQuery, TrainingYearVm>
{
    public GetTrainingYearVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<TrainingYearVm> Handle(TrainingYearVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingYears.SingleOrDefaultAsync(x => x.TrainingYearId == request.Id, cancellationToken);
        return _mapper.Map<TrainingYear, TrainingYearVm>(entity);
    }
}
