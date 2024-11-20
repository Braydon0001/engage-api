namespace Engage.Application.Services.SurveyQuestionOptions.Commands;

public class CreateSurveyQuestionOptionCommand : SurveyQuestionOptionCommand, IRequest<OperationStatus>
{
    public int SurveyQuestionId { get; set; }
}

public class CreateSurveyQuestionOptionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyQuestionOptionCommand, OperationStatus>
{
    public CreateSurveyQuestionOptionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateSurveyQuestionOptionCommand command, CancellationToken cancellationToken)
    {
        var optionsCount = _context.SurveyQuestionOptions
                                .Where(x => x.SurveyQuestionId == command.SurveyQuestionId)
                                .Count();

        var entity = _mapper.Map<CreateSurveyQuestionOptionCommand, SurveyQuestionOption>(command);
        entity.DisplayOrder = optionsCount + 1;
        entity.SurveyQuestionId = command.SurveyQuestionId;
        _context.SurveyQuestionOptions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.SurveyQuestionOptionId;

        return opStatus;
    }
}
