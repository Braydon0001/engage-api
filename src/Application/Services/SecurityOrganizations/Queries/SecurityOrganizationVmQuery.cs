namespace Engage.Application.Services.SecurityOrganizations.Queries;

public record SecurityOrganizationVmQuery(int Id) : IRequest<SecurityOrganizationVm>;

public record SecurityOrganizationVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityOrganizationVmQuery, SecurityOrganizationVm>
{
    public async Task<SecurityOrganizationVm> Handle(SecurityOrganizationVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityOrganizations.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.SecurityOrganizationId == query.Id, cancellationToken);

        var user = await Context.Users.Where(e => e.UserId == entity.OwnerId).SingleOrDefaultAsync(cancellationToken);

        var mappedEntity = entity == null ? null : Mapper.Map<SecurityOrganizationVm>(entity);

        mappedEntity.OwnerId = new OptionDto(user.UserId, user.FullName);

        return mappedEntity;
    }
}