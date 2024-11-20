using Engage.Application.Services.TrainingProviders.Models;

namespace Engage.Application.Services.TrainingProviders.Queries
{
    public class TrainingProvidersQuery : GetQuery, IRequest<ListResult<TrainingProviderDto>>
    {
    }

    public class TrainingProvidersQueryHandler : BaseQueryHandler, IRequestHandler<TrainingProvidersQuery, ListResult<TrainingProviderDto>>
    {
        public TrainingProvidersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<TrainingProviderDto>> Handle(TrainingProvidersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.TrainingProviders
                                                         .OrderByDescending(e => e.TrainingProviderId)
                                                         .ProjectTo<TrainingProviderDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

            return new ListResult<TrainingProviderDto>(entities);
        }
    }
}
