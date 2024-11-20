using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.SurveyInstances.Queries;

public class SurveyInstanceQuery : GetByIdQuery, IRequest<SurveyInstanceDto>
{
}

public class SurveyInstanceQueryHandler : BaseQueryHandler, IRequestHandler<SurveyInstanceQuery, SurveyInstanceDto>
{
    public SurveyInstanceQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyInstanceDto> Handle(SurveyInstanceQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyInstances.Include(x => x.SurveyAnswers)
                                                   .SingleAsync(x => x.SurveyInstanceId == request.Id, cancellationToken);

        return _mapper.Map<SurveyInstance, SurveyInstanceDto>(entity);
    }
}
