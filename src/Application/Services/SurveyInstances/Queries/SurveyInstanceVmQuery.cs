using Engage.Application.Services.SurveyAnswers.Queries;
using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.SurveyInstances.Queries
{
    public class SurveyInstanceVmQuery : GetByNullableIdQuery, IRequest<SurveyInstanceVM>
    {
    }

    public class SurveyInstanceVmQueryHandler : BaseViewModelQueryHandler, IRequestHandler<SurveyInstanceVmQuery, SurveyInstanceVM>
    {
        public SurveyInstanceVmQueryHandler(IMediator mediator) : base(mediator) { }

        public async Task<SurveyInstanceVM> Handle(SurveyInstanceVmQuery request, CancellationToken cancellationToken)
        {
            var vm = new SurveyInstanceVM();

            var id = 0;
            if (request.Id.HasValue)
            {
                id = request.Id.Value;
                vm.SurveyInstance = await _mediator.Send(new SurveyInstanceQuery() { Id = request.Id.Value });
            }

            vm.Employees = await _mediator.Send(new OptionsQuery(nameof(vm.Employees)));
            vm.Stores = await _mediator.Send(new OptionsQuery(nameof(vm.Stores)));
            vm.Surveys = await _mediator.Send(new OptionsQuery(nameof(vm.Surveys)));
            vm.Answers = await _mediator.Send(new SurveyAnswersQuery() { SurveyInstanceId = id });

            return vm;
        }
    }
}
