using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.ClaimStatusUsers.Models;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.ClaimStatusUsers.Queries
{
    public class GetClaimStatusUsersQuery : GetQuery, IRequest<ListResult<ClaimStatusUserDto>>  
    {
        public int? UserId { get; set; }
        public int? ClaimStatusId { get; set; }
    }

    public class GetClaimStatusUserQueryHandler : BaseQueryHandler, IRequestHandler<GetClaimStatusUsersQuery, ListResult<ClaimStatusUserDto>>
    {
        public GetClaimStatusUserQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<ClaimStatusUserDto>> Handle(GetClaimStatusUsersQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.ClaimStatusUsers.AsQueryable();

            if (request.ClaimStatusId.HasValue)
            {
                queryable = queryable.Where(e => e.ClaimStatusId == request.ClaimStatusId);
            }

            if (request.UserId.HasValue)
            {
                queryable = queryable.Where(e => e.UserId == request.UserId);
            }

            var entities = await queryable.OrderBy(e => e.ClaimStatusUserId)  
                                          .ProjectTo<ClaimStatusUserDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new ListResult<ClaimStatusUserDto>
            {
                Count = entities.Count,
                Data = entities
            }; 
        }
    }
}
