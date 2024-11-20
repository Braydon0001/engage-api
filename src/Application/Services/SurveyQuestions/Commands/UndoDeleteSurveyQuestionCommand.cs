using Engage.Application.Services.Shared.AssignCommands;
using Engage.Application.Services.SurveyQuestions.Models;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SurveyQuestions.Commands;

public class UndoDeleteSurveyQuestionCommand : IRequest<OperationStatus>
{
    public int SurveyQuestionId { get; set; }
    public int SurveyId { get; set; }
    public int DisplayOrder { get; set; }
}

public class UndoDeleteSurveyQuestionCommandHandler : IRequestHandler<UndoDeleteSurveyQuestionCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    public UndoDeleteSurveyQuestionCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(UndoDeleteSurveyQuestionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyQuestions.Where(e => e.SurveyQuestionId == command.SurveyQuestionId).IgnoreQueryFilters().FirstOrDefaultAsync();

        if (entity != null)
        {
            entity.DisplayOrder = command.DisplayOrder;
            await _mediator.Send(new DeleteCommand
            {
                EntityName = "surveyquestion",
                Id = command.SurveyQuestionId,
                Undo = true
            });

            var followingQuestions = await _context.SurveyQuestions.Where(e => e.SurveyId == command.SurveyId).Where(e => e.DisplayOrder >= command.DisplayOrder).Where(e => e.SurveyQuestionId != command.SurveyQuestionId).ToListAsync();
            foreach (var question in followingQuestions)
            {
                question.DisplayOrder = question.DisplayOrder + 1;
            }
        }
        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }

}



