namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public record SurveyFormAnswerVmQuery(int Id) : IRequest<SurveyFormAnswerVm>;

public record SurveyFormAnswerVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerVmQuery, SurveyFormAnswerVm>
{
    public async Task<SurveyFormAnswerVm> Handle(SurveyFormAnswerVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormAnswers.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SurveyFormSubmission)
                             .Include(e => e.SurveyFormQuestion)
                             .Include(e => e.SurveyFormReason);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormAnswerId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormAnswerVm>(entity);
    }
}