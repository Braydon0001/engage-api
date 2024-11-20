namespace Engage.Application.Services.ProjectTacopsBoard.Queries;

public class ProjectStoreTacopsBoardQuery : IRequest<Board>
{
    public int? RegionId { get; set; }
    public int? StoreId { get; set; }
    public int? ProjectId { get; set; }
    public string Search { get; set; }
}

public record ProjectStoreTacopsBoardHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreTacopsBoardQuery, Board>
{
    public async Task<Board> Handle(ProjectStoreTacopsBoardQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStores.AsQueryable().AsNoTracking();

        if (query.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == query.StoreId.Value);
        }

        if (query.ProjectId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectId == query.ProjectId.Value);
        }

        if (query.RegionId.HasValue)
        {
            queryable = queryable.Where(e => e.Store.EngageRegionId == query.RegionId.Value);
        }

        var storeProjects = await queryable.Include(e => e.Store)
                                           .ToListAsync(cancellationToken);

        List<int> projectIds = storeProjects.Select(e => e.ProjectId).ToList();

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
            Title = statutes.Name,
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
                                            .ToListAsync(cancellationToken);

        Dictionary<int, string> imageUrls = new();
        if (tasksList.Count > 0)
        {
            foreach (var item in tasksList)
            {
                var user = await Context.Users.FirstOrDefaultAsync(e => e.Email == item.CreatedBy, cancellationToken);
                if (user != null)
                {
                    var employee = await Context.Employees.FirstOrDefaultAsync(e => e.UserId == user.UserId, cancellationToken);
                    if (employee != null)
                    {
                        //imageUrls = employee.Files?.Where(e => e.Type.ToLower() == "photo").FirstOrDefault()?.Url;
                        imageUrls.Add(item.ProjectTaskId, employee.Files?.Where(e => e.Type.ToLower() == "photo").FirstOrDefault()?.Url);
                    }
                }
            }
        }

        tasksList.ForEach(task => tasks.Add(task.ProjectTaskId, new
        {
            Id = task.ProjectTaskId,
            projectId = task.ProjectId,
            Content = projectIds.Contains(task.ProjectId)
                ? $"{task.Project.Name} ({storeProjects.FirstOrDefault(e => e.ProjectId == task.ProjectId).Store.Name}) - {task.Project.ProjectPriority.Name}"
                : $"{task.Project.Name} - {task.Project.ProjectPriority.Name}",
            Description = $"{task.Name} - {task.ProjectTaskPriority.Name}",
            TaskPriorityId = task.ProjectTaskPriorityId,
            //TaskSeverityId = task.ProjectTaskSeverityId,
            TaskTypeId = task.ProjectTaskTypeId,
            TaskStatusId = task.ProjectTaskStatusId,
            task.Project.ProjectPriorityId,
            createdDate = task.Created,
            createdBy = task.CreatedBy,
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
