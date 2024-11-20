namespace Engage.Application.Services.SurveyFormOptions.Queries;

public record SurveyFormOptionVmQuery(int Id) : IRequest<SurveyFormOptionVm>;

public record SurveyFormOptionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormOptionVmQuery, SurveyFormOptionVm>
{
    public async Task<SurveyFormOptionVm> Handle(SurveyFormOptionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormOptions.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormOptionId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormOptionVm>(entity);
    }
}