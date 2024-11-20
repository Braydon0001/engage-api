using Engage.Application.Services.TrainingYears.Models;

namespace Engage.Application.Services.TrainingYears.Queries;

public class TrainingYearsQuery : GetQuery, IRequest<ListResult<TrainingYearDto>>
{
}

public class TrainingYearsQueryHandler : BaseQueryHandler, IRequestHandler<TrainingYearsQuery, ListResult<TrainingYearDto>>
{
    public TrainingYearsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<TrainingYearDto>> Handle(TrainingYearsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.TrainingYears.OrderBy(e => e.TrainingYearId)
                                                .ProjectTo<TrainingYearDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<TrainingYearDto>(entities);
    }
}
