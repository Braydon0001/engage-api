namespace Engage.Application.Services.EmployeeStoreKpis.Commands;

public class EmployeeStoreKpiCommand : IMapTo<EmployeeStoreKpi>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int EmployeeKpiId { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public int Score { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreKpiCommand, EmployeeStoreKpi>();
    }
}

