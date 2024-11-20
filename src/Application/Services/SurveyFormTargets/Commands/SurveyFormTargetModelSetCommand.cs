namespace Engage.Application.Services.SurveyFormTargets.Commands;

public class SurveyFormTargetModelSetCommand : IMapTo<JsonRule>, IRequest<JsonRule>
{
    public JsonRule Model { get; set; }
    public int Id { get; set; }
}

public record SurveyFormTargetModelSetCommandHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTargetModelSetCommand, JsonRule>
{
    public async Task<JsonRule> Handle(SurveyFormTargetModelSetCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.FirstOrDefaultAsync(e => e.SurveyFormId == command.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception("Entity not found");
        }

        var rules = new List<JsonRule>(entity.Rules ?? []);

        var rule = rules.Where(e => e.Type == "TargetRule").FirstOrDefault();

        if (rules == null || rules.Count == 0)
        {
            if (command.Model == null)
            {
                return new JsonRule();
            }
            var newRule = new JsonRule() { Type = "TargetRule", Value = command.Model.Value };
            rules = new List<JsonRule>([newRule]);
        }
        else
        {
            if (rule != null)
            {
                if (command.Model == null)
                {
                    rules.Remove(rule);
                }
                else
                {
                    rule.Value = command.Model.Value;
                }

            }
            else
            {
                if (command.Model != null)
                {
                    rules.Add(new JsonRule() { Type = "TargetRule", Value = command.Model.Value });
                }
            }
        }
        entity.Rules = rules;

        await Context.SaveChangesAsync(cancellationToken);

        var targetRule = entity.Rules.Where(e => e.Type == "TargetRule").FirstOrDefault();

        return targetRule ?? new JsonRule();
    }
}

public class SurveyFormTargetModelSetValidator : AbstractValidator<SurveyFormTargetModelSetCommand>
{
    public SurveyFormTargetModelSetValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}