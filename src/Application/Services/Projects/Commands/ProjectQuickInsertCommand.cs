namespace Engage.Application.Services.Projects.Commands;

public class ProjectQuickInsertCommand : IMapTo<Project>, IRequest<Project>
{
    public string ProjectName { get; set; }
    public string TaskName { get; set; }
    public string TaskComment { get; set; }
    public int ProjectTypeId { get; set; }
    public int ProjectStatusId { get; set; }
    public DateTime? StartDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectQuickInsertCommand, Project>()
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ProjectName));
    }
}
public record ProjectQuickInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectQuickInsertCommand, Project>
{
    public async Task<Project> Handle(ProjectQuickInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectQuickInsertCommand, Project>(command);

        entity.ProjectPriorityId = (int)ProjectPriorityId.Default;

        Context.Projects.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status && command.TaskName.IsNotEmpty())
        {
            ProjectTask task = new ProjectTask()
            {
                Name = command.TaskName,
                ProjectId = entity.ProjectId,
                ProjectTaskStatusId = (int)ProjectTaskStatusId.Open,
                ProjectTaskPriorityId = (int)ProjectTaskPriorityId.Default
            };

            Context.ProjectTasks.Add(task);

            var taskStatus = await Context.SaveChangesAsync(cancellationToken);

            if (taskStatus.Status && command.TaskComment.IsNotEmpty())
            {
                ProjectTaskNote taskComment = new ProjectTaskNote
                {
                    ProjectTaskId = task.ProjectTaskId,
                    Note = command.TaskComment
                };

                Context.ProjectTaskNotes.Add(taskComment);

                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        return entity;
    }
}