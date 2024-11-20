using Engage.Application.Services.WebEvents.Models;

namespace Engage.Application.Services.EmployeeWeb.Models;
public class EmployeeWebVm
{
    public List<KpiCard> KpiCards { get; set; }
    public List<WebEventDto> Notifications { get; set; }
}

public class KpiCard
{
    public string Heading { get; set; }
    public int Score { get; set; }
    public int? KpiTierId { get; set; }
}
