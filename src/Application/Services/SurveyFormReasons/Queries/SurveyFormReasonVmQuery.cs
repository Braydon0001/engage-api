namespace Engage.Application.Services.SurveyFormReasons.Queries;

public record SurveyFormReasonVmQuery(int Id) : IRequest<SurveyFormReasonVm>;

public record SurveyFormReasonVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormReasonVmQuery, SurveyFormReasonVm>
{
    public async Task<SurveyFormReasonVm> Handle(SurveyFormReasonVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormReasons.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormReasonId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormReasonVm>(entity);
    }
}