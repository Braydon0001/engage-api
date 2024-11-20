using Engage.Application.Services.SurveyAnswerOptions.Models;

namespace Engage.Application.Services.SurveyAnswerOptions.Queries;

public class GetSurveyAnswerOptionViewModelQuery : GetByNullableIdQuery, IRequest<SurveyAnswerOptionVm>
{
}


public class GetSurveyAnswerOptionViewModelQueryHandler : BaseViewModelQueryHandler, IRequestHandler<GetSurveyAnswerOptionViewModelQuery, SurveyAnswerOptionVm>
{
    public GetSurveyAnswerOptionViewModelQueryHandler(IMediator mediator) : base(mediator) { }

    public async Task<SurveyAnswerOptionVm> Handle(GetSurveyAnswerOptionViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new SurveyAnswerOptionVm();

        if (request.Id.HasValue)
        {
            vm.SurveyAnswerOption = await _mediator.Send(new GetSurveyAnswerOptionQuery() { Id = request.Id.Value });
        }

        return vm;
    }
}
