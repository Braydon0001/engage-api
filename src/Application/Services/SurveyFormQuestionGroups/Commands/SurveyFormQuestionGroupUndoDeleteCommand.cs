namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupUndoDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int DisplayOrder { get; set; }

}

public record SurveyFormQuestionGroupUndoDeleteHandler(IAppDbContext Context, IMediator Mediator) : IRequestHandler<SurveyFormQuestionGroupUndoDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionGroupUndoDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormQuestionGroupId == command.Id).IgnoreQueryFilters().FirstOrDefaultAsync();

        if (entity != null)
        {
            entity.DisplayOrder = command.DisplayOrder;
            await Mediator.Send(new DeleteCommand
            {
                EntityName = "surveyformquestiongroup",
                Id = command.Id,
                Undo = true
            });

            var followingGroups = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormId == entity.SurveyFormId
                                                                                    && e.DisplayOrder >= command.DisplayOrder
                                                                                    && e.SurveyFormQuestionGroupId != entity.SurveyFormQuestionGroupId)
                                                                        .ToListAsync();
            foreach (var group in followingGroups)
            {
                group.DisplayOrder = group.DisplayOrder + 1;
            }
        }
        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}

public class SurveyFormQuestionGroupUndoDeleteValidator : AbstractValidator<SurveyFormQuestionGroupUndoDeleteCommand>
{
    public SurveyFormQuestionGroupUndoDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DisplayOrder).NotEmpty().GreaterThan(0);
    }
}