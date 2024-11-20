namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupRuleCommand : IRequest<OperationStatus>
{
    public JsonRule Model { get; set; }
    public int Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupRuleCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupRuleHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupRuleCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionGroupRuleCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormQuestionGroupId == command.Id).FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var rules = new List<JsonRule>(entity.Rules ?? []);

        var rule = rules.Where(e => e.Type == command.Model.Type).FirstOrDefault();

        if (rule == null && command.Model.Value != null)
        {
            rules.Add(command.Model);
        }
        else if (rule != null && command.Model.Value != null)
        {
            rule.Value = command.Model.Value;
        }
        else if (rule != null && command.Model.Value == null)
        {
            rules.Remove(rule);
        }

        entity.Rules = rules;
        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class UpdateSurveyFormQuestionGroupRuleValidator : AbstractValidator<SurveyFormQuestionGroupRuleCommand>
{
    public UpdateSurveyFormQuestionGroupRuleValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Model);
    }
}