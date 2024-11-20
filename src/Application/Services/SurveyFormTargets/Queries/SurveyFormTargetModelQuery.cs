namespace Engage.Application.Services.SurveyFormTargets.Queries;

public class SurveyFormTargetModelQuery : IRequest<JsonRule>
{
    public int Id { get; set; }
}

public class SurveyFormTargetModelHandler : IRequestHandler<SurveyFormTargetModelQuery, JsonRule>
{
    private readonly IAppDbContext _context;

    public SurveyFormTargetModelHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
    }

    public async Task<JsonRule> Handle(SurveyFormTargetModelQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        if (entity.Rules == null)
        {
            return new JsonRule();
        }

        var targetingRule = entity.Rules.Where(e => e.Type == "TargetRule").FirstOrDefault();

        return targetingRule ?? new JsonRule();
    }
}
