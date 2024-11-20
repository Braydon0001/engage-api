namespace Engage.Application.Services.ProjectTasks.Queries;

public class ProjectTasksBoardQuery : IRequest<Board>
{
    public int? RegionId { get; set; }
    public int? ProjectId { get; set; }
    public string Search { get; set; }
}

public record ProjectTasksBoardHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTasksBoardQuery, Board>
{
    public async Task<Board> Handle(ProjectTasksBoardQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Projects.Where(e => EF.Property<string>(e, "Discriminator") == "Project")
                                        .AsQueryable()
                                        .AsNoTracking();

        if (query.ProjectId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectId == query.ProjectId.Value);
        }

        if (query.RegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == query.RegionId.Value);
        }

        var projects = await queryable.ToListAsync(cancellationToken);

        List<int> projectIds = projects.Select(e => e.ProjectId).ToList();

        var tasksQuaryable = Context.ProjectTasks.Include(e => e.Project)
                                                 .AsQueryable()
                                                 .AsNoTracking()
                                                 .Where(e => projectIds.Contains(e.ProjectId) && e.IsClosed == false);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            tasksQuaryable = tasksQuaryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Search}%")
                                                    || EF.Functions.Like(e.Project.Name, $"%{query.Search}%"));
        }

        var statuses = await Context.ProjectTaskStatuses.AsQueryable()
                                                        .AsNoTracking()
                                                        .ToListAsync(cancellationToken);

        var columns = new Dictionary<int, object>();

        statuses.ForEach(statutes => columns.Add(statutes.ProjectTaskStatusId, new
        {
            Id = statutes.ProjectTaskStatusId,
            Title = statutes.ProjectTaskStatusId == (int)ProjectTaskStatusId.Open ? "TODO" : statutes.ProjectTaskStatusId == (int)ProjectTaskStatusId.Assigned ? "IN PROGRESS" : "COMPLETED",
            TaskIds = tasksQuaryable.Where(p => p.ProjectTaskStatusId == statutes.ProjectTaskStatusId)
                                    .OrderByDescending(e => e.ProjectTaskPriorityId)
                                    .ThenByDescending(e => e.Project.ProjectPriorityId)
                                    .Select(p => p.ProjectTaskId)
                                    .ToList()
        }));

        var tasks = new Dictionary<int, object>();

        var tasksList = await tasksQuaryable.Include(e => e.Project)
                                                .ThenInclude(e => e.ProjectPriority)
                                            .Include(e => e.ProjectTaskPriority)
                                            .Include(e => e.ProjectTaskProjectStakeholderUsers)
                                                .ThenInclude(e => e.ProjectStakeholder)
                                                    .ThenInclude(e => e.User)
                                            .ToListAsync(cancellationToken);

        Dictionary<int, string> imageUrls = new();
        if (tasksList.Count > 0)
        {
            foreach (var item in tasksList)
            {
                if (item.ProjectTaskProjectStakeholderUsers.Count > 0)
                {
                    var userEmail = item.ProjectTaskProjectStakeholderUsers.First().ProjectStakeholder.User.Email;
                    var user = await Context.Users.FirstOrDefaultAsync(e => e.Email == userEmail, cancellationToken);
                    if (user != null)
                    {
                        var employee = await Context.Employees.FirstOrDefaultAsync(e => e.UserId == user.UserId, cancellationToken);
                        if (employee != null)
                        {
                            imageUrls.Add(item.ProjectTaskId, employee.Files?.Where(e => e.Type.ToLower() == "photo").FirstOrDefault()?.Url);
                        }
                    }
                }

            }
        }

        tasksList.ForEach(task => tasks.Add(task.ProjectTaskId, new
        {
            Id = task.ProjectTaskId,
            projectId = task.ProjectId,
            Content = projectIds.Contains(task.ProjectId)
                ? $"{task.Project.Name} - {task.Project.ProjectPriority.Name}"
                : $"{task.Project.Name} - {task.Project.ProjectPriority.Name}",
            Description = $"{task.Name} - {task.ProjectTaskPriority.Name}",
            TaskPriorityId = task.ProjectTaskPriorityId,
            TaskTypeId = task.ProjectTaskTypeId,
            TaskStatusId = task.ProjectTaskStatusId,
            task.Project.ProjectPriorityId,
            createdDate = task.Created,
            createdBy = task.CreatedBy,
            assignedTo = task.ProjectTaskProjectStakeholderUsers.Count > 0 ? task.ProjectTaskProjectStakeholderUsers.First().ProjectStakeholder.User.Email : null,
            UserPhotoUrl = imageUrls.FirstOrDefault(e => e.Key == task.ProjectTaskId).Value
        }));

        var columnOrder = statuses.Select(e => e.ProjectTaskStatusId);

        return new Board
        {
            Tasks = tasks,
            Columns = columns,
            ColumnOrder = columnOrder.ToList()
        };
    }
}
