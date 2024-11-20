using Engage.Application.Services.TrainingTypes.Models;

namespace Engage.Application.Services.TrainingTypes.Queries;

public class TrainingTypeVmQuery : GetByIdQuery, IRequest<TrainingTypeVm>
{
}

public class TrainingTypeVmQueryHandler : BaseQueryHandler, IRequestHandler<TrainingTypeVmQuery, TrainingTypeVm>
{
    public TrainingTypeVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingTypeVm> Handle(TrainingTypeVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingTypes.SingleAsync(e => e.TrainingTypeId == request.Id, cancellationToken);

        return _mapper.Map<TrainingType, TrainingTypeVm>(entity);
    }
}
