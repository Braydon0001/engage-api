namespace Engage.Application.Services.SurveyForms.Queries;

public record SurveyFormVmQuery(int Id) : IRequest<SurveyFormVm>;

public record SurveyFormVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormVmQuery, SurveyFormVm>
{
    public async Task<SurveyFormVm> Handle(SurveyFormVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyForms.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SurveyFormType)
                             .Include(e => e.EngageSubGroup)
                             .Include(e => e.Supplier)
                             .Include(e => e.EngageBrand)
                             .Include(e => e.SurveyFormProducts)
                                .ThenInclude(e => e.EngageMasterProduct);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormId == query.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var vm = Mapper.Map<SurveyFormVm>(entity);

        var linkedProducts = entity.SurveyFormProducts.Select(p => new OptionDto() { Id = p.EngageMasterProductId, Name = p.EngageMasterProduct.Name + " - " + p.EngageMasterProduct.Code }).ToList();

        vm.EngageMasterProductIds = linkedProducts;

        return vm;
    }
}