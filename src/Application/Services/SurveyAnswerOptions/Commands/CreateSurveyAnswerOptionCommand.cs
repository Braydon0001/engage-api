namespace Engage.Application.Services.SurveyAnswerOptions.Commands;

public class CreateSurveyAnswerOptionCommand : SurveyAnswerOptionCommand, IRequest<OperationStatus>
{
    public bool SaveChanges { get; set; } = true;
}

public class CreateSurveyAnswerOptionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyAnswerOptionCommand, OperationStatus>
{
    public CreateSurveyAnswerOptionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateSurveyAnswerOptionCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSurveyAnswerOptionCommand, SurveyAnswerOption>(command);
        _context.SurveyAnswerOptions.Add(entity);

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
