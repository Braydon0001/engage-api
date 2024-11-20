using Engage.Application.Services.ClaimStatusUsers.Models;

namespace Engage.Application.Services.ClaimStatusUsers.Queries
{
    public class GetClaimStatusUserVmQuery: GetByIdQuery, IRequest<ClaimStatusUserVm>
    {
        
    }

    public class GetClaimUserVmQueryHandler : BaseQueryHandler, IRequestHandler<GetClaimStatusUserVmQuery, ClaimStatusUserVm>
    {
        public GetClaimUserVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ClaimStatusUserVm> Handle(GetClaimStatusUserVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimStatusUsers.Include(e => e.User)                                                  
                                                  .SingleAsync(e => e.ClaimStatusUserId == request.Id, cancellationToken);

            return _mapper.Map<ClaimStatusUser, ClaimStatusUserVm>(entity);
        }
    }
}
