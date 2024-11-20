using Engage.Application.Services.SurveyQuestionOptions.Models;

namespace Engage.Application.Services.SurveyQuestionOptions.Queries;

public class SurveyQuestionOptionQuery : GetByIdQuery, IRequest<SurveyQuestionOptionDto>
{
}

public class SurveyQuestionOptionQueryHandler : BaseQueryHandler, IRequestHandler<SurveyQuestionOptionQuery, SurveyQuestionOptionDto>
{

    public SurveyQuestionOptionQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<SurveyQuestionOptionDto> Handle(SurveyQuestionOptionQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyQuestionOptions.SingleAsync(x => x.SurveyQuestionOptionId == request.Id, cancellationToken);
        return _mapper.Map<SurveyQuestionOption, SurveyQuestionOptionDto>(entity);
    }
}
