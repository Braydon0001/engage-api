using Engage.Application.Services.SurveyAnswerOptions.Models;

namespace Engage.Application.Services.SurveyAnswerOptions.Queries;

public class GetSurveyAnswerOptionListQuery : GetQuery, IRequest<ListResult<SurveyAnswerOptionListItemDto>>
{
    public int? EmployeeStoreSurveyAnswerId { get; set; }
}


public class GetSurveyAnswerOptionListQueryHandler : BaseQueryHandler, IRequestHandler<GetSurveyAnswerOptionListQuery, ListResult<SurveyAnswerOptionListItemDto>>
{
    public GetSurveyAnswerOptionListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<SurveyAnswerOptionListItemDto>> Handle(GetSurveyAnswerOptionListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.SurveyAnswerOptions.Where(x => x.SurveyAnswerId == (request.EmployeeStoreSurveyAnswerId ?? x.SurveyAnswerId))
                                                         .OrderBy(x => x.SurveyQuestionOption)
                                                         .ProjectTo<SurveyAnswerOptionListItemDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

        return new ListResult<SurveyAnswerOptionListItemDto>(entities); 
    }
}
