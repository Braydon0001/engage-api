using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public record OfflineProjectStoreQuery : IRequest<List<ProjectStoreVm>>;

public class OfflineProjectStoreHandler : BaseQueryHandler, IRequestHandler<OfflineProjectStoreQuery, List<ProjectStoreVm>>
{
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    public OfflineProjectStoreHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user) : base(context, mapper)
    {
        _mediator = mediator;
        _user = user;
    }

    public async Task<List<ProjectStoreVm>> Handle(OfflineProjectStoreQuery query, CancellationToken cancellationToken)
    {
        var currentUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email.ToLower() == _user.UserName.ToLower(), cancellationToken)
            ?? throw new Exception("Current user not found");

        var userStakeholders = await _context.ProjectStakeholderUsers.Where(e => e.UserId == currentUser.UserId && e.Project.ProjectStatusId != (int)ProjectStatusId.Completed).ToListAsync(cancellationToken);

        var userStakeholderIds = userStakeholders.Select(e => e.ProjectStakeholderId).ToList();

        var assignedProjectIds = await _context.ProjectAssignees.Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId)).Select(e => e.ProjectId).ToListAsync(cancellationToken);

        var ownerProjectIds = await _context.ProjectStores.Where(e => e.OwnerId == currentUser.UserId).Select(e => e.ProjectId).ToListAsync(cancellationToken);

        var projectIds = assignedProjectIds.Union(ownerProjectIds).Distinct().ToList();

        var responses = new List<ProjectStoreVm>();

        foreach (var projectId in projectIds)
        {
            try
            {
                var response = await _mediator.Send(new ProjectStoreVmQuery
                (
                     projectId,
                     false

                ), cancellationToken);

                responses.Add(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }




        return responses;

    }
}