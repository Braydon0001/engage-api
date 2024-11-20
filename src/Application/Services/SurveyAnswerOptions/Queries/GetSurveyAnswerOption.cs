using Engage.Application.Services.SurveyAnswerOptions.Models;

namespace Engage.Application.Services.SurveyAnswerOptions.Queries;

public class GetSurveyAnswerOptionQuery : GetByIdQuery, IRequest<SurveyAnswerOptionDto>
{ 
}

public class GetSurveyAnswerOptionDtoQueryHandler : BaseQueryHandler, IRequestHandler<GetSurveyAnswerOptionQuery, SurveyAnswerOptionDto>
{
    public GetSurveyAnswerOptionDtoQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<SurveyAnswerOptionDto> Handle(GetSurveyAnswerOptionQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyAnswerOptions.FirstOrDefaultAsync(x => x.SurveyAnswerOptionId == request.Id, cancellationToken);

        return _mapper.Map<SurveyAnswerOption, SurveyAnswerOptionDto>(entity);
    }
}
