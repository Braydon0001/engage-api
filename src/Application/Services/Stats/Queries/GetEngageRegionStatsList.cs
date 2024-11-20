using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.Stats.Models;
using Engage.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Engage.Application.Services.Stats.Queries
{
    public class GetEngageRegionStatsListQuery : GetByIdQuery, IRequest<ListResult<StatsByEngageRegionListItemDto>>
    { }

    public class GetEngageRegionStatsListQueryHandler : BaseQueryHandler, IRequestHandler<GetEngageRegionStatsListQuery, ListResult<StatsByEngageRegionListItemDto>>
    {
        public GetEngageRegionStatsListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<StatsByEngageRegionListItemDto>> Handle(GetEngageRegionStatsListQuery request, CancellationToken cancellationToken)
        {
            var storesByRegions = await _context.StatsStoresByRegions.Include(x => x.EngageRegion).ToListAsync(cancellationToken);
            var ordersByRegions = await _context.StatsOrdersByRegions.ToListAsync(cancellationToken);

            List<StatsByEngageRegionListItemDto> combinedStats = new List<StatsByEngageRegionListItemDto>();
            foreach (var entity in storesByRegions)
            {
                combinedStats.Add(new StatsByEngageRegionListItemDto
                {
                    EngageRegionId = entity.EngageRegionId,
                    EngageRegionName = entity.EngageRegion.Name,
                    StoresCount = entity.Stores,
                    OrdersLast1Day = ordersByRegions.FirstOrDefault(x => x.EngageRegionId == entity.EngageRegionId).OrdersLast1Day,
                    OrdersLast7Days = ordersByRegions.FirstOrDefault(x => x.EngageRegionId == entity.EngageRegionId).OrdersLast7Days,
                    OrdersAll = ordersByRegions.FirstOrDefault(x => x.EngageRegionId == entity.EngageRegionId).OrdersAll,
                });
            };

            return new ListResult<StatsByEngageRegionListItemDto>
            {
                Data = combinedStats,
                Count = combinedStats.Count
            };
        }
    }
}
