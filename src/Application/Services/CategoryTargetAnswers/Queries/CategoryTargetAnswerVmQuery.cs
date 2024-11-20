namespace Engage.Application.Services.CategoryTargetAnswers.Queries;

public record CategoryTargetAnswerVmQuery(int Id) : IRequest<CategoryTargetAnswerVm>;

public record CategoryTargetAnswerVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetAnswerVmQuery, CategoryTargetAnswerVm>
{
    public async Task<CategoryTargetAnswerVm> Handle(CategoryTargetAnswerVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargetAnswers.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CategoryTarget);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryTargetAnswerId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryTargetAnswerVm>(entity);
    }
}