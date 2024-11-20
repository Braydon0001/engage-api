using Engage.Application.Services.Employees.Models;
using Engage.Application.Services.Stores.Models;
using Engage.Application.Services.Targetings.Enums;

namespace Engage.Application.Services.Targetings.Models;

public class CriteriaDto
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<OptionDto> Options { get; set; }
}

public class TargetingDto
{
    public int Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public TargetEntity TargetEntityId { get; set; }
    public string TargetEntityName { get; set; }
    public TargetingEntity TargetingEntityId { get; set; }
    public string TargetingEntityName { get; set; }
    public Dictionary<string, CriteriaDto> Criteria { get; set; }

    public TargetingDto()
    {
    }

    public TargetingDto(int id, string createdBy, DateTime created, string criteria, TargetEntity targetEntity)
    {
        Id = id;
        CreatedBy = createdBy;
        Created = created;
        TargetEntityId = targetEntity;
        TargetEntityName = Enum.GetName(typeof(TargetEntity), targetEntity);
        Criteria = JsonConvert.DeserializeObject<Dictionary<string, CriteriaDto>>(criteria);
    }
}

public class StoreTargetingDto : TargetingDto
{
    public List<StoreListDto> Stores { get; set; }

    public StoreTargetingDto()
    {
    }

    public StoreTargetingDto(int id, string createdBy, DateTime created, string criteria, TargetEntity targetEntity) :
        base(id, createdBy, created, criteria, targetEntity)
    {
        TargetingEntityId = TargetingEntity.Store;
        TargetingEntityName = Enum.GetName(typeof(TargetingEntity), TargetingEntity.Store);
    }
}

public class EmployeeTargetingDto : TargetingDto
{
    public List<EmployeeListDto> Employees { get; set; }

    public EmployeeTargetingDto()
    {
    }

    public EmployeeTargetingDto(int id, string createdBy, DateTime created, string criteria, TargetEntity targetEntity) :
        base(id, createdBy, created, criteria, targetEntity)
    {
        TargetingEntityId = TargetingEntity.Employee;
        TargetingEntityName = Enum.GetName(typeof(TargetingEntity), TargetingEntity.Employee);
    }
}
