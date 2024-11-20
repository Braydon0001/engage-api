using System.Threading;
using System.Threading.Tasks;
using Engage.Application.Services.Options.Descriptions;
using Engage.Application.Services.Options.Queries;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.Surveys.Models;
using MediatR;

namespace Engage.Application.Services.Surveys.Queries
{
    public class SurveyTargetingVMQuery : GetByNullableIdQuery, IRequest<SurveyTargetingVM>
    { }

    public class SurveyTargetingVMQueryHandler : BaseViewModelQueryHandler, IRequestHandler<SurveyTargetingVMQuery, SurveyTargetingVM>
    {
        public SurveyTargetingVMQueryHandler(IMediator mediator) : base(mediator) { }

        public async Task<SurveyTargetingVM> Handle(SurveyTargetingVMQuery request, CancellationToken cancellationToken)
        {
            var vm = new SurveyTargetingVM();
            var id = 0;

            if (request.Id.HasValue)
            {
                id = request.Id.Value;
                vm.Survey = await _mediator.Send(new SurveyQuery() { Id = id });
            };

            vm.UnassignedEngageRegions = await _mediator.Send(new GetUnassignedOptionListQuery(UnassignedOptionDesc.SURVEYENGAGEREGIONS, id));            

            return vm;
        }
    }
}
