using Engage.Application.Services.SurveyQuestionOptions.Models;

namespace Engage.Application.Services.SurveyQuestionOptions.Queries;

public class SurveyQuestionOptionVmQuery : GetByNullableIdQuery, IRequest<SurveyQuestionOptionVM>
{
}

public class SurveyQuestionOptionVmQueryHandler : BaseViewModelQueryHandler, IRequestHandler<SurveyQuestionOptionVmQuery, SurveyQuestionOptionVM>
{
    public SurveyQuestionOptionVmQueryHandler(IMediator mediator) : base(mediator) { }

    public async Task<SurveyQuestionOptionVM> Handle(SurveyQuestionOptionVmQuery request, CancellationToken cancellationToken)
    {
        var vm = new SurveyQuestionOptionVM();
        if (request.Id.HasValue)
        {
            vm.SurveyQuestionOption = await _mediator.Send(new SurveyQuestionOptionQuery()
            {
                Id = request.Id.Value
            }, cancellationToken);
        };

        return vm;
    }
}
