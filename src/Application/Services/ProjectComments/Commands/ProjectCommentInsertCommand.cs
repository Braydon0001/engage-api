namespace Engage.Application.Services.ProjectComments.Commands;

public class ProjectCommentInsertCommand : IMapTo<ProjectComment>, IRequest<ProjectComment>
{
    public int ProjectId { get; init; }
    public string Comment { get; init; }
    public bool HasFiles { get; init; }
    public bool CompleteIncident { get; init; }
    public DateTime? CompletedDate { get; init; }
    public string CreatedBy { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCommentInsertCommand, ProjectComment>();
    }
}

public record ProjectCommentInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectCommentInsertCommand, ProjectComment>
{
    public async Task<ProjectComment> Handle(ProjectCommentInsertCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.Include(p => p.Store)
                                                 .Include(p => p.Owner)
                                                 .Include(p => p.ProjectPriority)
                                                 .Include(p => p.ProjectType)
                                                 .Include(p => p.ProjectSubType)
                                                 .Include(p => p.ProjectCategory)
                                                 .Include(p => p.ProjectSubCategory)
                                                 .Include(p => p.ProjectSuppliers)
                                                    .ThenInclude(s => s.Supplier)
                                                 .FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken) ?? throw new Exception("Project not found");

        var entity = Mapper.Map<ProjectCommentInsertCommand, ProjectComment>(command);

        if (command.CreatedBy.IsNotNullOrWhiteSpace())
        {
            entity.CreatedBy = command.CreatedBy;
        }

        if (command.CompleteIncident)
        {
            Context.projectStatusHistories.Add(new ProjectStatusHistory
            {
                ProjectId = entity.ProjectId,
                ProjectStatusId = project.ProjectStatusId,
                Reason = command.Comment
            });
            project.ProjectStatusId = (int)ProjectStatusId.Completed;
            project.ClosedDate = command.CompletedDate ?? DateTime.Now.AddHours(2); //GMT +2
        }

        entity.ProjectStatusId = project.ProjectStatusId;

        Context.ProjectComments.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        //if (opStatus.Status)
        //{
        //    if (!command.HasFiles)
        //    {
        //        await Mediator.Send(new ProjectCommentSendCommunicationCommand { ProjectCommentId = entity.ProjectCommentId });
        //    }

        //}

        return entity;
    }
}

public class ProjectCommentInsertValidator : AbstractValidator<ProjectCommentInsertCommand>
{
    public ProjectCommentInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Comment).NotEmpty().MaximumLength(1000);
    }
}