namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderListQuery : IRequest<List<ProjectStakeholderDto>>
{
    public int ProjectId { get; set; }
}

public record ProjectStakeholderSelectListHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStakeholderListQuery, List<ProjectStakeholderDto>>
{
    public async Task<List<ProjectStakeholderDto>> Handle(ProjectStakeholderListQuery query, CancellationToken cancellationToken)
    {
        if (query.ProjectId < 1)
        {
            throw new Exception("Project not found");
        }

        var project = await Context.Projects.FindAsync(query.ProjectId);

        if (project == null)
        {
            throw new Exception("Project not found");
        }

        var stakeholderUsers = await Context.ProjectStakeholderUsers.Where(p => p.ProjectId == query.ProjectId)
                                                                    .ProjectTo<ProjectStakeholderDto>(Mapper.ConfigurationProvider)
                                                                    .ToListAsync(cancellationToken);

        var stakeholderStoreContacts = await Context.ProjectStakeholderStoreContacts.Where(p => p.ProjectId == query.ProjectId)
                                                                                    .ProjectTo<ProjectStakeholderDto>(Mapper.ConfigurationProvider)
                                                                                    .ToListAsync(cancellationToken);

        var stateholderSupplierContacts = await Context.ProjectStakeholderSupplierContacts.Where(p => p.ProjectId == query.ProjectId)
                                                                                          .ProjectTo<ProjectStakeholderDto>(Mapper.ConfigurationProvider)
                                                                                          .ToListAsync(cancellationToken);

        var stakeholderRegionContacts = await Context.ProjectStakeholderEmployeeRegionContacts.Where(p => p.ProjectId == query.ProjectId)
                                                                                      .ProjectTo<ProjectStakeholderDto>(Mapper.ConfigurationProvider)
                                                                                      .ToListAsync(cancellationToken);

        var stakeholderExternalUsers = await Context.ProjectStakeholderExternalUsers.Where(p => p.ProjectId == query.ProjectId)
                                                                                    .ProjectTo<ProjectStakeholderDto>(Mapper.ConfigurationProvider)
                                                                                    .ToListAsync(cancellationToken);

        var stakeholderList = stakeholderUsers.Union(stakeholderStoreContacts)
                                             .Union(stateholderSupplierContacts)
                                             .Union(stakeholderRegionContacts)
                                             .Union(stakeholderExternalUsers)
                                             .ToList();

        return stakeholderList;
    }
}
