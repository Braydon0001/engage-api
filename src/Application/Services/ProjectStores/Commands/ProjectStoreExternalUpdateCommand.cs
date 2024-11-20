namespace Engage.Application.Services.ProjectStores.Commands;

public class ProjectStoreExternalUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string ProjectStatus { get; set; }
    public string ProjectComment { get; set; }
    public string ProjectStatusId { get; set; }
    public string CreatedBy { get; init; }
}

public record ProjectStoreExternalUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStoreExternalUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreExternalUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectStores.SingleOrDefaultAsync(e => e.ProjectId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var opstatus = new OperationStatus();

        if (int.TryParse(command.ProjectStatus, out int statusId))
        {
            if (statusId != (int)ProjectStatusId.Assigned && command.ProjectComment.IsNullOrEmpty())
            {
                throw new Exception("Error no comment found");

            }
            if (statusId == (int)ProjectStatusId.Unassigned)
            {
                Context.projectStatusHistories.Add(new ProjectStatusHistory
                {
                    ProjectId = entity.ProjectId,
                    ProjectStatusId = entity.ProjectStatusId,
                    Reason = command.ProjectComment
                });
                entity.ProjectStatusId = statusId;
            }

            if (command.ProjectComment.IsNotNullOrEmpty())
            {
                var comment = new ProjectComment
                {
                    ProjectId = entity.ProjectId,
                    ProjectStatusId = statusId,
                    Comment = command.ProjectComment,
                    CreatedBy = command.CreatedBy,
                };
                Context.ProjectComments.Add(comment);

                await Context.SaveChangesAsync(cancellationToken);

                opstatus.ReturnObject = comment.ProjectCommentId;
            }
        }

        //await Mediator.Send(new ProjectStoreSendCommunicationCommand { ProjectId = command.Id });

        opstatus.Status = true;
        opstatus.OperationId = command.Id;

        return opstatus;
    }
}

public class ProjectStoreExternalUpdateValidator : AbstractValidator<ProjectStoreExternalUpdateCommand>
{
    public ProjectStoreExternalUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}