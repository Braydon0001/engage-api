namespace Engage.Application.Services.DistributionCenters.Commands;

public class DistributionCenterCommand : IMapTo<DistributionCenter>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public List<int> WarehouseIds { get; set; }
    public List<int> DepartmentIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DistributionCenterCommand, DistributionCenter>()
            .ForMember(e => e.Warehouses, opt => opt.Ignore());
    }
}
