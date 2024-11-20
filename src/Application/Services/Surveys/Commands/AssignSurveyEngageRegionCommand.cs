using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Surveys.Commands;

public class AssignSurveyEngageRegionCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public int EngageRegionId { get; set; }
}

public class AssignSurveyEngageRegionHandler : BaseCreateCommandHandler, IRequestHandler<AssignSurveyEngageRegionCommand, OperationStatus>
{
    public AssignSurveyEngageRegionHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(AssignSurveyEngageRegionCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AssignCommand(AssignDesc.REGION_SURVEY, command.SurveyId, command.EngageRegionId), cancellationToken);

        return new OperationStatus()
        {
            Status = true
        };
    }
}
