namespace Engage.Application.Services.TargetStrategies.Queries;

public class TargetStrategyOptionListQuery : IRequest<List<TargetStrategyOption>>
{

}

public class TargetStrategyOptionListHandler : ListQueryHandler, IRequestHandler<TargetStrategyOptionListQuery, List<TargetStrategyOption>>
{
    public TargetStrategyOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<TargetStrategyOption>> Handle(TargetStrategyOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.TargetStrategies.AsQueryable().AsNoTracking();

        return await queryable.ProjectTo<TargetStrategyOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
