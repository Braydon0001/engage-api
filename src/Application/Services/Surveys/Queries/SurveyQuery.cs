using Engage.Application.Services.Surveys.Models;

namespace Engage.Application.Services.Surveys.Queries;

public class SurveyQuery : GetByIdQuery, IRequest<SurveyDto>
{
}

public class SurveyQueryHandler : BaseQueryHandler, IRequestHandler<SurveyQuery, SurveyDto>
{

    public SurveyQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<SurveyDto> Handle(SurveyQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.Include(x => x.EngageSubGroup)
                                           .Include(x => x.SurveyEngageRegions)
                                           .ThenInclude(x => x.EngageRegion)
                                           .FirstOrDefaultAsync(x => x.SurveyId == request.Id, cancellationToken);

        return _mapper.Map<Survey, SurveyDto>(entity);
    }
}
