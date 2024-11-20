using Engage.Application.Services.SurveyAnswers.Models;
using Engage.Application.Services.SurveyQuestions.Queries;

namespace Engage.Application.Services.SurveyAnswers.Queries;

public class SurveyAnswerVmQuery : GetByNullableIdQuery, IRequest<SurveyAnswerVM>
{
}

public class SurveyAnswerVmQueryHandler : BaseViewModelQueryHandler, IRequestHandler<SurveyAnswerVmQuery, SurveyAnswerVM>
{
    public SurveyAnswerVmQueryHandler(IMediator mediator) : base(mediator)
    {
    }

    public async Task<SurveyAnswerVM> Handle(SurveyAnswerVmQuery request, CancellationToken cancellationToken)
    {
        var vm = new SurveyAnswerVM();

        if (request.Id.HasValue)
        {
            vm.SurveyAnswer = await _mediator.Send(new SurveyAnswerQuery() { Id = request.Id.Value });
        }

        vm.SurveyQuestion = await _mediator.Send(new SurveyQuestionVmQuery() { Id = vm.SurveyAnswer.SurveyQuestionId }, cancellationToken);

        return vm;
    }
}
