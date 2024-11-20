namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderSearchOptionDto
{
    public string GroupName { get; set; }
    public List<ProjectStakeholderSearchOption> Options { get; set; }

}

public class ProjectStakeholderSearchOption
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Identifier { get; set; }
    public string PhotoUrl { get; set; }
}