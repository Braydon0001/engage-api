namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStoreExternalUserVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsAssigned { get; set; }
    public bool IsStakeholder { get; set; }
}
