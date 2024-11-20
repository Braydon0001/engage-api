using Engage.Application.Services.ProjectTacopsBoard.Queries;
using Newtonsoft.Json.Linq;

namespace Engage.Application.Services.ProjectTacopsBoard.Commands;

public class ProjectStoreTacopsBoardUpdateCommand : IRequest<OperationStatus>
{
    public Board Board { get; set; }
}

public record ProjectStoreTacopsBoardUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options) : IRequestHandler<ProjectStoreTacopsBoardUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreTacopsBoardUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command.Board == null)
        {
            throw new Exception("No data was recieved");
        }

        var statusIds = command.Board.Columns.Keys.ToList();

        foreach (var statusId in statusIds)
        {

            var column = command.Board.Columns[statusId] as JObject;
            var val = column.GetValue("taskIds") ?? throw new Exception("Cannot find taskIds");

            //convert val to list of int
            var taskIds = val.ToObject<List<int>>();

            foreach (var taskId in taskIds)
            {
                var task = await Context.ProjectTasks.SingleOrDefaultAsync(e => e.ProjectTaskId == taskId, cancellationToken);

                if (task == null)
                {
                    throw new Exception("Task not found");
                }

                task.ProjectTaskStatusId = statusId;
            }
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        //return opStatus;

        return new OperationStatus();
    }
}
