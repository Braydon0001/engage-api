namespace Engage.Application.Services.SurveyFormTypes.Queries;

public record SurveyFormTypeVmQuery(int Id) : IRequest<SurveyFormTypeVm>;

public record SurveyFormTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTypeVmQuery, SurveyFormTypeVm>
{
    public async Task<SurveyFormTypeVm> Handle(SurveyFormTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormTypeVm>(entity);
    }
}