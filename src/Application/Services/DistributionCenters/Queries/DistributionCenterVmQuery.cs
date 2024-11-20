using Engage.Application.Services.DistributionCenters.Models;

namespace Engage.Application.Services.DistributionCenters.Queries
{
    public class DistributionCenterVmQuery : IRequest<DistributionCenterVm>
    {
        public int Id { get; set; }
    }

    public class DistributionCenterVmQueryHandler : BaseQueryHandler, IRequestHandler<DistributionCenterVmQuery, DistributionCenterVm>
    {
        public DistributionCenterVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<DistributionCenterVm> Handle(DistributionCenterVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.DistributionCenters.SingleAsync(x => x.DistributionCenterId == request.Id, cancellationToken);
            return _mapper.Map<DistributionCenter, DistributionCenterVm>(entity);
        }
    }
}
