namespace Engage.Application.Services.ProjectTasks.Queries;

public class Board
{
    public Dictionary<int, object> Columns { get; set; }
    public Dictionary<int, object> Tasks { get; set; }
    public List<int> ColumnOrder { get; set; }
}
