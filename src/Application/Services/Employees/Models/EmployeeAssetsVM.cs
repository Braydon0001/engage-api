namespace Engage.Application.Services.Employees.Models;

public class EmployeeAssetsVM: IMapFrom<Employee>
{
    public EmployeeAssetsVM ()
    {
        Assets = new List<EmployeeAssetVM> ();
    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool HasAssets { get; set; }

    public List<EmployeeAssetVM> Assets { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeAssetsVM>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName));
    }
}

public class EmployeeAssetVM : IMapFrom<EmployeeAsset>, IMapFrom<EmployeeVehicle>, IMapFrom<EmployeeCoolerBox>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string AssetType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeAsset, EmployeeAssetVM>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeAssetId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

        profile.CreateMap<EmployeeVehicle, EmployeeAssetVM>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeVehicleId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

        profile.CreateMap<EmployeeCoolerBox, EmployeeAssetVM>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeCoolerBoxId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}

