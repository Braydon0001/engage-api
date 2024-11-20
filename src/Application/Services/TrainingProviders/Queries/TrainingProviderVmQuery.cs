using Engage.Application.Services.TrainingProviders.Models;

namespace Engage.Application.Services.TrainingProviders.Queries;

public class TrainingProviderVmQuery : GetByIdQuery, IRequest<TrainingProviderVm>
{
}

public class TrainingProviderVmQueryHandler : BaseQueryHandler, IRequestHandler<TrainingProviderVmQuery, TrainingProviderVm>
{
    public TrainingProviderVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingProviderVm> Handle(TrainingProviderVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingProviders.SingleAsync(e => e.TrainingProviderId == request.Id, cancellationToken);

        return _mapper.Map<TrainingProvider, TrainingProviderVm>(entity);
    }
}
