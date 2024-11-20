using Engage.Application.Services.EmployeeHrStatistics.Model;

namespace Engage.Application.Services.EmployeeHrStatistic.Queries;

public class EmployeeHrStatisticsQuery : IRequest<EmployeeHrStatisticsDto>
{
    public int? RegionId { get; set; }
}

public record EmployeeHrStatisticsQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeHrStatisticsQuery, EmployeeHrStatisticsDto>
{
    public async Task<EmployeeHrStatisticsDto> Handle(EmployeeHrStatisticsQuery query, CancellationToken cancellationToken)
    {
        var employees = await Context.Employees.AsNoTracking()
                                                .Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee && !e.Disabled && !e.Deleted
                                                            && (!query.RegionId.HasValue
                                                               || e.EmployeeRegions.Any(er => er.EngageRegionId == query.RegionId.Value)))
                                                .Include(e => e.EmployeeRace)
                                                .ToListAsync(cancellationToken);

        var totalHeadCount = employees.Count();
        var maleHeadCount = employees.Where(e => e.EmployeeGenderId == 1).Count();
        var femaleHeadCount = employees.Where(e => e.EmployeeGenderId == 2).Count();

        // 'No Disability' option Id for Engage is 1 and Encore is 9
        var abledHeadCount = employees.Where(e => e.EmployeeDisabledTypeId == 1).Count();
        var disabledHeadCount = employees.Where(e => e.EmployeeDisabledTypeId != 1).Count();

        var today = DateTime.Today;

        #region Employee Tenure Calculations
        // Calculates tenure in full years i.e if the full year has not been completed, it is not counted
        var employeeTenures = employees.Select(e =>
        {
            var endDate = e.EndDate ?? today;
            var years = endDate.Year - e.StartingDate.Year;
            // If the current year's month-day is before the starting month-day, subtract a year
            if (endDate.Month < e.StartingDate.Month || (endDate.Month == e.StartingDate.Month && endDate.Day < e.StartingDate.Day))
            {
                years--;
            }
            return years;
        }).ToList();

        var avgTenure = employeeTenures.Average();

        List<EmployeeBarChartItemDto> tenureChartData = new()
        {
            BuildEmployeeBarChartItem("10YRS +", employeeTenures.Count(t => t > 9)),
            BuildEmployeeBarChartItem("5-9YRS", employeeTenures.Count(t => t > 4 && t < 10)),
            BuildEmployeeBarChartItem("< 5YRS", employeeTenures.Count(t => t < 5))
        };
        #endregion


        #region Employee Age Calculations
        //calculates age in full years i.e if the full year has not been completed, it is not counted
        //If the employee's birthday this year is still in the future, then 1 is subtracted from the preliminary age

        var employeeAges = employees.Select(e =>
        {
            var hasBirthdayPassedThisYear = e.DateOfBirth.Month < today.Month ||
                                    (e.DateOfBirth.Month == today.Month && e.DateOfBirth.Day <= today.Day);

            var ages = today.Year - e.DateOfBirth.Year - (hasBirthdayPassedThisYear ? 0 : 1);

            return ages;
        }).ToList();

        var avgAge = employeeAges.Average();

        List<EmployeeBarChartItemDto> ageChartData = new()
        {
            BuildEmployeeBarChartItem("< 35YRS", employeeAges.Count(a => a < 35)),
            BuildEmployeeBarChartItem("35-55YRS", employeeAges.Count(a => a >= 35 && a <= 55)),
            BuildEmployeeBarChartItem("> 55YRS", employeeAges.Count(a => a > 55))
        };

        var raceChartData = employees.GroupBy(e => e.EmployeeRaceId)
                                    .Select(group => BuildEmployeePieChartItem(group.First().EmployeeRace?.Name, group.Count()))
                                    .ToList();
        #endregion

        var employeeStatistics = new EmployeeHrStatisticsDto
        {
            HeadCount = new HeadCountDto
            {
                Total = BuildStatCard("Head Count", totalHeadCount),
                Male = BuildStatCard("Male", maleHeadCount),
                Female = BuildStatCard("Female", femaleHeadCount),
                Disabled = BuildStatCard("Disabled", disabledHeadCount),
                Abled = BuildStatCard("Abled", abledHeadCount)
            },
            Race = new RaceDto
            {
                PieChartData = raceChartData
            },
            Tenure = new TenureDto
            {
                Average = BuildStatCard("Avg Tenure", Math.Round(avgTenure, 1)),
                BarChartData = tenureChartData
            },
            Age = new AgeDto
            {
                Average = BuildStatCard("Avg Age", Math.Round(avgAge)),
                BarChartData = ageChartData
            }
        };

        return employeeStatistics;
    }

    private StatCardDto BuildStatCard(string label, double value)
    {
        return new StatCardDto
        {
            Label = label,
            Value = value
        };
    }

    private EmployeeBarChartItemDto BuildEmployeeBarChartItem(string label, int value)
    {
        return new EmployeeBarChartItemDto
        {
            Label = label,
            Employees = value
        };
    }

    private EmployeePieChartItemDto BuildEmployeePieChartItem(string name, int value)
    {
        return new EmployeePieChartItemDto
        {
            Name = name,
            Value = value,
        };
    }
}

