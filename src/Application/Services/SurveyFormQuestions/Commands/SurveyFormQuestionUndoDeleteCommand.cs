namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionUndoDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int DisplayOrder { get; set; }

}

public record SurveyFormQuestionUndoDeleteHandler(IAppDbContext Context, IMediator Mediator) : IRequestHandler<SurveyFormQuestionUndoDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionUndoDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup).Where(e => e.SurveyFormQuestionId == command.Id).IgnoreQueryFilters().FirstOrDefaultAsync();

        if (entity != null)
        {
            entity.DisplayOrder = command.DisplayOrder;
            await Mediator.Send(new DeleteCommand
            {
                EntityName = "surveyformquestion",
                Id = command.Id,
                Undo = true
            });

            var followingQuestions = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionGroupId == entity.SurveyFormQuestionGroupId).Where(e => e.DisplayOrder >= command.DisplayOrder).Where(e => e.SurveyFormQuestionId != command.Id).ToListAsync();
            foreach (var question in followingQuestions)
            {
                question.DisplayOrder = question.DisplayOrder + 1;
            }
        }
        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}

public class SurveyFormQuestionUndoDeleteValidator : AbstractValidator<SurveyFormQuestionUndoDeleteCommand>
{
    public SurveyFormQuestionUndoDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DisplayOrder).NotEmpty().GreaterThan(0);
    }
}