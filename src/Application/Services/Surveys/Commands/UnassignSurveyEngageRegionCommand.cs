using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Surveys.Commands;

public class UnassignSurveyEngageRegionCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public int EngageRegionId { get; set; }
}

public class UnassignSurveyEngageRegionHandler : BaseCreateCommandHandler, IRequestHandler<UnassignSurveyEngageRegionCommand, OperationStatus>
{
    public UnassignSurveyEngageRegionHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(UnassignSurveyEngageRegionCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UnassignCommand(AssignDesc.REGION_SURVEY, command.SurveyId, command.EngageRegionId), cancellationToken);

        return new OperationStatus()
        {
            Status = true
        };
    }
}
