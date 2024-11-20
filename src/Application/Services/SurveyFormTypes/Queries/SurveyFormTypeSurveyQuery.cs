namespace Engage.Application.Services.SurveyFormTypes.Queries;

public record SurveyFormTypeSurveyQuery(int Id) : IRequest<SurveyFormTypeVm>;

public record SurveyFormTypeSurveyQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTypeSurveyQuery, SurveyFormTypeVm>
{
    public async Task<SurveyFormTypeVm> Handle(SurveyFormTypeSurveyQuery query, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.Include(e => e.SurveyFormType)
                                              .Where(e => e.SurveyFormId == query.Id)
                                              .Select(e => e.SurveyFormType)
                                              .FirstOrDefaultAsync(cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormTypeVm>(entity);
    }
}