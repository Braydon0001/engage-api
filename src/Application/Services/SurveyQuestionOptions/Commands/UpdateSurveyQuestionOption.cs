namespace Engage.Application.Services.SurveyQuestionOptions.Commands;

public class UpdateSurveyQuestionOptionCommand : SurveyQuestionOptionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateSurveyQuestionOptionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSurveyQuestionOptionCommand, OperationStatus>
{
    public UpdateSurveyQuestionOptionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }
    public async Task<OperationStatus> Handle(UpdateSurveyQuestionOptionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyQuestionOptions.SingleAsync(x => x.SurveyQuestionOptionId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.SurveyQuestionOptionId;
        return opStatus;
    }
}
