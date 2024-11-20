namespace Engage.Application.Services.SurveyAnswerOptions.Commands;

public class UpdateSurveyAnswerOptionCommand : SurveyAnswerOptionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class UpdateSurveyAnswerOptionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSurveyAnswerOptionCommand, OperationStatus>
{
    public UpdateSurveyAnswerOptionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateSurveyAnswerOptionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyAnswerOptions.SingleAsync(x => x.SurveyAnswerOptionId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.SurveyAnswerOptionId;
            return opStatus;
        }
        return new OperationStatus
        {
            Status = true
        };
    }
}
