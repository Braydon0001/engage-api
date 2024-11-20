namespace Engage.Application.Services.SurveyFormSubmissions.Queries;

public record SurveyFormSubmissionVmQuery(int Id) : IRequest<SurveyFormSubmissionVm>;

public record SurveyFormSubmissionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormSubmissionVmQuery, SurveyFormSubmissionVm>
{
    public async Task<SurveyFormSubmissionVm> Handle(SurveyFormSubmissionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormSubmissions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.User)
                             .Include(e => e.SurveyForm)
                             .Include(e => e.Store);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormSubmissionId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormSubmissionVm>(entity);
    }
}