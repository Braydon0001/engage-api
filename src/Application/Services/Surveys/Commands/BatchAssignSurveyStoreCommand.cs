using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Surveys.Commands;

public class BatchAssignSurveyStoreCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public List<int> Stores { get; set; }
}

public class BatchAssignSurveyStoreHandler : BaseCreateCommandHandler, IRequestHandler<BatchAssignSurveyStoreCommand, OperationStatus>
{
    public BatchAssignSurveyStoreHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(BatchAssignSurveyStoreCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(new BatchAssignCommand(AssignDesc.STORE_SURVEY, command.SurveyId, command.Stores), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return new OperationStatus()
        {
            Status = true
        };
    }
}
