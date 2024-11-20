using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.StoreSurveys.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Engage.Application.Services.StoreSurveys.Queries
{
    public class GetSurveyStoresByEngageRegion : GetQuery, IRequest<ListResult<SelectedStoreDto>>
    {
        public int SurveyId { get; set; }
        public int EngageRegionId { get; set; }
    }

    public class GetSurveyStoresByEngageRegionQueryHandler : BaseQueryHandler, IRequestHandler<GetSurveyStoresByEngageRegion, ListResult<SelectedStoreDto>>
    {
        public GetSurveyStoresByEngageRegionQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<SelectedStoreDto>> Handle(GetSurveyStoresByEngageRegion request, CancellationToken cancellationToken)
        {
            var stores = await _context.Stores
                .Where(x => x.EngageRegionId == request.EngageRegionId)
                .OrderBy(x => x.StoreId)
                .ProjectTo<SelectedStoreDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var result = new ListResult<SelectedStoreDto>()
            {
                Count = stores.Count,
                Data = stores
            };

            var surveyStores = await _context.SurveyStores
                .Where(x =>
                      x.SurveyId == request.SurveyId)
                .OrderBy(x => x.StoreId)
                .ProjectTo<SurveyStoreDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (surveyStores != null && surveyStores.Count > 0)
            {
                foreach (var surveyStore in surveyStores)
                {
                    var store = stores.Find(x => x.Id == surveyStore.StoreId);
                    if (store != null)
                    {
                        store.Selected = true;
                    }
                }

                return new ListResult<SelectedStoreDto>()
                {
                    Count = stores.Count,
                    Data = stores.OrderByDescending(x => x.Selected)
                      .ThenBy(x => x.Id)
                      .ToList()
                };
            }

            return result;
        }
    }
}
