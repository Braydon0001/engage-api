using Engage.Application.Services.TrainingTypes.Models;

namespace Engage.Application.Services.TrainingTypes.Queries
{
    public class TrainingTypesQuery : GetQuery, IRequest<ListResult<TrainingTypeDto>>
    {
    }

    public class TrainingTypesQueryHandler : BaseQueryHandler, IRequestHandler<TrainingTypesQuery, ListResult<TrainingTypeDto>>
    {
        public TrainingTypesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<TrainingTypeDto>> Handle(TrainingTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.TrainingTypes
                                                         .OrderByDescending(e => e.TrainingTypeId)
                                                         .ProjectTo<TrainingTypeDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

            return new ListResult<TrainingTypeDto>(entities);
        }
    }
}
