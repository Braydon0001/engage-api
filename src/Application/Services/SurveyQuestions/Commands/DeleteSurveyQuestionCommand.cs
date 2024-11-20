using Engage.Application.Services.Shared.AssignCommands;
using Engage.Application.Services.SurveyQuestions.Models;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SurveyQuestions.Commands;

public class DeleteSurveyQuestionCommand : IRequest<OperationStatus>
{
    public int SurveyQuestionId { get; set; }
    public int SurveyId { get; set; }
    public int DisplayOrder { get; set; }
}

public class DeleteSurveyQuestionCommandHandler : IRequestHandler<DeleteSurveyQuestionCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    public DeleteSurveyQuestionCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) 
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(DeleteSurveyQuestionCommand command, CancellationToken cancellationToken)
    {
        var surveyQuestions = await _context.SurveyQuestions.Where(e => e.SurveyId == command.SurveyId).Include(e => e.Rules).ToListAsync(cancellationToken);

        var hasDependentRules = surveyQuestions.Any(x => x.Rules.Any(y => y.TargetQuestionId == command.SurveyQuestionId));

        if (hasDependentRules)
        {
            throw new Exception("Question is used in other rules");
        }

        var entity = surveyQuestions.Where(e => e.SurveyQuestionId == command.SurveyQuestionId).FirstOrDefault();

        if (entity != null)
        {
            entity.DisplayOrder = null;
            await _mediator.Send(new DeleteCommand
            {
                EntityName = "surveyquestion",
                Id = command.SurveyQuestionId,
            });

            var followingQuestions = await _context.SurveyQuestions.Where(e => e.SurveyId == command.SurveyId).Where(e => e.DisplayOrder > command.DisplayOrder).ToListAsync();
            foreach (var question in followingQuestions)
            {
                question.DisplayOrder = question.DisplayOrder - 1;
            }
        }
        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;

    }

}



