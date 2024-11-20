namespace Engage.Application.Services.EmployeeStores.Commands;

public class EmployeeStoreCommand : IMapTo<EmployeeStore>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCommand, EmployeeStore>();
    }
}
