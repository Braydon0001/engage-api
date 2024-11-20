using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.SurveyInstances.Queries
{
    public class SurveyInstancesQuery : GetQuery, IRequest<ListResult<SurveyInstanceListItemDto>>
    {
    }

    public class SurveyInstancesQueryHandler : BaseQueryHandler, IRequestHandler<SurveyInstancesQuery, ListResult<SurveyInstanceListItemDto>>
    {
        public SurveyInstancesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<SurveyInstanceListItemDto>> Handle(SurveyInstancesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.SurveyInstances.OrderBy(x => x.StoreId)
                                                                .ThenBy(x => x.Survey.SupplierId)
                                                                .ProjectTo<SurveyInstanceListItemDto>(_mapper.ConfigurationProvider)
                                                                .ToListAsync(cancellationToken);

            return new ListResult<SurveyInstanceListItemDto>(entities);
        }
    }
}
