namespace Engage.Application.Services.EmployeeHrStatistics.Model;

public class EmployeeHrStatisticsDto
{
    public HeadCountDto HeadCount { get; set; }
    public RaceDto Race { get; set; }
    public TenureDto Tenure { get; set; }
    public AgeDto Age { get; set; }
}



public class HeadCountDto
{
    public StatCardDto Total { get; set; }
    public StatCardDto Male { get; set; }
    public StatCardDto Female { get; set; }
    public StatCardDto Abled { get; set; }
    public StatCardDto Disabled { get; set; }
}

public class TenureDto
{
    public StatCardDto Average { get; set; }
    public List<EmployeeBarChartItemDto> BarChartData { get; set; }
}

public class AgeDto
{
    public StatCardDto Average { get; set; }
    public List<EmployeeBarChartItemDto> BarChartData { get; set; }
}

public class RaceDto
{
    public List<EmployeePieChartItemDto> PieChartData { get; set; }
}