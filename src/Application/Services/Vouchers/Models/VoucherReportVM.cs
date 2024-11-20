

using Engage.Application.Services.VoucherDetails.Models;

namespace Engage.Application.Services.Vouchers.Models;

public class VoucherReportVM<T>
{
    public int Count { get; set; }
    public object ReportName { get; set; }
    public List<T> Data { get; set; }

    public List<string> ColumnNames { get; set; }
}
