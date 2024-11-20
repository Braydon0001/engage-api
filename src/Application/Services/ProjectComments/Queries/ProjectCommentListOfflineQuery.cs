namespace Engage.Application.Services.ProjectComments.Queries;

public class ProjectCommentListOfflineQuery : IRequest<List<ProjectCommentDto>>
{
    public int UserId { get; set; }
}

public record ProjectCommentListOfflineHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCommentListOfflineQuery, List<ProjectCommentDto>>
{
    public async Task<List<ProjectCommentDto>> Handle(ProjectCommentListOfflineQuery query, CancellationToken cancellationToken)
    {

        var userStakeholderIds = await Context.ProjectStakeholderUsers.Where(e => e.UserId == query.UserId && e.Disabled != true)
                                                                      .Select(e => e.ProjectStakeholderId)
                                                                      .ToListAsync(cancellationToken);


        var projectIds = await Context.ProjectAssignees.AsNoTracking()
                                                       .Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                       .Include(e => e.ProjectStakeholder)
                                                       .Select(e => e.ProjectId)
                                                       .ToListAsync(cancellationToken);

        var ownerProjectIds = await Context.ProjectStores.Where(e => e.OwnerId == query.UserId).Select(e => e.ProjectId).ToListAsync(cancellationToken);


        projectIds.AddRange(ownerProjectIds);

        var queryable = Context.ProjectComments.AsQueryable().AsNoTracking().Where(e => projectIds.Contains(e.ProjectId));

        var data = await queryable.OrderByDescending(e => e.Created)
                              .ProjectTo<ProjectCommentDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        if (data.Count > 0)
        {
            foreach (var item in data)
            {
                var user = await Context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == item.CreatedBy.ToLower(), cancellationToken);
                if (user != null)
                {
                    var employee = await Context.Employees.FirstOrDefaultAsync(e => e.UserId == user.UserId, cancellationToken);
                    if (employee != null)
                    {
                        item.UserPhotoUrl = employee.Files?.Where(e => e.Type.ToLower() == "photo").FirstOrDefault()?.Url;
                    }
                }
            }
        }


        return data;
    }
}