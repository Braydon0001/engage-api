using Engage.Application.Services.Trainings.Models;

namespace Engage.Application.Services.Trainings.Queries
{
    public class TrainingsQuery : GetQuery, IRequest<ListResult<TrainingDto>>
    {
    }

    public class TrainingsQueryHandler : BaseQueryHandler, IRequestHandler<TrainingsQuery, ListResult<TrainingDto>>
    {
        public TrainingsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<TrainingDto>> Handle(TrainingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Trainings
                                                   .OrderByDescending(e => e.TrainingId)
                                                   .ProjectTo<TrainingDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

            return new ListResult<TrainingDto>(entities);
        }
    }
}
