namespace Engage.Application.Services.ProjectExternalUsers.Queries;

public record ProjectExternalUserVmQuery(int Id) : IRequest<ProjectExternalUserVm>;

public record ProjectExternalUserVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectExternalUserVmQuery, ProjectExternalUserVm>
{
    public async Task<ProjectExternalUserVm> Handle(ProjectExternalUserVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectExternalUsers.AsQueryable().AsNoTracking().Include(e => e.ExternalUserType).Include(e => e.CommunicationTypes);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectExternalUserId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectExternalUserVm>(entity);
    }
}