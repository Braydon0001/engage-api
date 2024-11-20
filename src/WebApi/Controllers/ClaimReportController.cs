using Engage.Application.Services.ClaimReports.Commands;
using OfficeOpenXml;

namespace Engage.WebApi.Controllers;

public class FinanceReportParams
{
    //Required
    public List<int> ClaimClassificationIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public int ClaimPeriodId { get; set; }

    //Optional
    public int? SupplierId { get; set; }
    public int? ToClaimPeriodId { get; set; }
    public List<int> ClaimTypeIds { get; set; }
    public int? StoreId { get; set; }
    public int? ClaimAccountManagerId { get; set; }
    public int? ClaimManagerId { get; set; }
    public int? ProductSupplierId { get; set; }
}

public class SummaryReportParams
{
    //Required
    public List<int> ClaimClassificationIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public int ClaimPeriodId { get; set; }
    public List<int> SupplierIds { get; set; }

    //Optional    
    public int? ToClaimPeriodId { get; set; }
    public List<int> ClaimTypeIds { get; set; }
    public int? StoreId { get; set; }
    public int? ClaimAccountManagerId { get; set; }
    public int? ClaimManagerId { get; set; }
    public int? ProductSupplierId { get; set; }
}

public class ExtraReportParams : FinanceReportParams
{
    public int ClaimStatusId { get; set; }
    public bool IsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public int? ClaimSupplierStatusId { get; set; }
}

public class RelatedReportParams
{
    //Required
    //public int ClaimClassificationId { get; set; }
    public List<int> ClaimClassificationIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public int ClaimPeriodId { get; set; }

    //Optional
    public int? SupplierId { get; set; }
    public int? ToClaimPeriodId { get; set; }
    public List<int> ClaimTypeIds { get; set; }
    public int? StoreId { get; set; }
    public int? ClaimAccountManagerId { get; set; }
    public int? ClaimManagerId { get; set; }
    public int? ProductSupplierId { get; set; }
}
public class ClaimReportController : BaseController
{
    [HttpPost]
    public async Task<FileStreamResult> GenerateClaimReport([FromBody] GenerateClaimReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Claims");

            workSheet.Cells.LoadFromCollection(result.Data, true);

            var rowCount = workSheet.Dimension.Rows;
            if (rowCount > 0)
            {
                rowCount = rowCount + 2;
                if (result.ReportSubTotals.Count > 0)
                {
                    foreach (var total in result.ReportSubTotals)
                    {
                        workSheet.Cells[rowCount, 1].Value = total.Name;
                        workSheet.Cells[rowCount, 2].Value = total.Value;
                        rowCount = rowCount + 1;
                    }
                }
            }

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);

    }


    [HttpPost("Finance")]
    public async Task<IActionResult> GetAll(FinanceReportParams financeReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var classificationId in financeReportParams.ClaimClassificationIds)
            {
                var command = new GenerateClaimReportCommand
                {
                    ClaimClassificationId = classificationId,
                    EngageRegionIds = financeReportParams.EngageRegionIds,
                    ClaimPeriodId = financeReportParams.ClaimPeriodId,
                    SupplierId = financeReportParams.SupplierId,
                    ToClaimPeriodId = financeReportParams.ToClaimPeriodId,
                    ClaimTypeIds = financeReportParams.ClaimTypeIds,
                    StoreId = financeReportParams.StoreId,
                    ClaimAccountManagerId = financeReportParams.ClaimAccountManagerId,
                    ClaimManagerId = financeReportParams.ClaimManagerId,
                    ProductSupplierId = financeReportParams.ProductSupplierId,
                };

                var result = await Mediator.Send(command);

                var workSheet = package.Workbook.Worksheets.Add(result.ClassificationName);
                workSheet.Cells.LoadFromCollection(result.Data, true);

                var rowCount = workSheet.Dimension.Rows;
                if (rowCount > 0)
                {
                    rowCount = rowCount + 2;
                    if (result.ReportSubTotals.Count > 0)
                    {
                        foreach (var total in result.ReportSubTotals)
                        {
                            workSheet.Cells[rowCount, 1].Value = total.Name;
                            workSheet.Cells[rowCount, 2].Value = total.Value;
                            rowCount = rowCount + 1;
                        }
                    }
                }
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Finance Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("Finance2")]
    public async Task<IActionResult> GetFinanceReport2(FinanceReportParams financeReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var regionId in financeReportParams.EngageRegionIds)
            {
                var command = new GenerateClaimFinanceReportCommand
                {
                    ClaimClassificationIds = financeReportParams.ClaimClassificationIds,
                    EngageRegionId = regionId,
                    ClaimPeriodId = financeReportParams.ClaimPeriodId,
                    SupplierId = financeReportParams.SupplierId,
                    ToClaimPeriodId = financeReportParams.ToClaimPeriodId,
                    ClaimTypeIds = financeReportParams.ClaimTypeIds,
                    StoreId = financeReportParams.StoreId,
                    ClaimAccountManagerId = financeReportParams.ClaimAccountManagerId,
                    ClaimManagerId = financeReportParams.ClaimManagerId,
                    ProductSupplierId = financeReportParams.ProductSupplierId,
                };

                var result = await Mediator.Send(command);

                var workSheet = package.Workbook.Worksheets.Add(result.RegionName);
                workSheet.Cells.LoadFromCollection(result.Data, true);

                var rowCount = workSheet.Dimension.Rows;
                if (rowCount > 0)
                {
                    rowCount = rowCount + 2;
                    if (result.ReportSubTotals.Count > 0)
                    {
                        foreach (var total in result.ReportSubTotals)
                        {
                            workSheet.Cells[rowCount, 1].Value = total.Name;
                            workSheet.Cells[rowCount, 2].Value = total.Value;
                            rowCount = rowCount + 1;
                        }
                    }
                }
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Finance Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("summary")]
    public async Task<IActionResult> GetSummaryReport(SummaryReportParams summaryReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("SUMMARY");

            foreach (var regionId in summaryReportParams.EngageRegionIds)
            {
                foreach (var classificationId in summaryReportParams.ClaimClassificationIds)
                {
                    var command = new GenerateClaimSummaryReportCommand
                    {
                        ClaimClassificationId = classificationId,
                        EngageRegionId = regionId,
                        ClaimPeriodId = summaryReportParams.ClaimPeriodId,
                        SupplierIds = summaryReportParams.SupplierIds,
                        ToClaimPeriodId = summaryReportParams.ToClaimPeriodId,
                        ClaimTypeIds = summaryReportParams.ClaimTypeIds,
                        StoreId = summaryReportParams.StoreId,
                        ClaimAccountManagerId = summaryReportParams.ClaimAccountManagerId,
                        ClaimManagerId = summaryReportParams.ClaimManagerId,
                        ProductSupplierId = summaryReportParams.ProductSupplierId,
                    };

                    var result = await Mediator.Send(command);

                    var r = workSheet.Dimension;
                    if (r != null)
                    {
                        workSheet.Cells[r.Rows + 2, 1].Value = result.RegionName + " - " + result.ClassificationName;
                    }
                    else
                    {
                        workSheet.Cells[1, 1].Value = result.RegionName + " - " + result.ClassificationName;
                    }

                    workSheet.Cells["A" + (workSheet.Dimension.Rows + 1)].LoadFromCollection(result.Data, false);
                }
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Summary Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("extras")]
    public async Task<IActionResult> GetExtraReport(ExtraReportParams extraReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var regionId in extraReportParams.EngageRegionIds)
            {
                var command = new GenerateClaimExtraReportCommand
                {
                    ClaimClassificationIds = extraReportParams.ClaimClassificationIds,
                    EngageRegionId = regionId,
                    ClaimPeriodId = extraReportParams.ClaimPeriodId,
                    SupplierId = extraReportParams.SupplierId,
                    ToClaimPeriodId = extraReportParams.ToClaimPeriodId,
                    ClaimTypeIds = extraReportParams.ClaimTypeIds,
                    StoreId = extraReportParams.StoreId,
                    ClaimAccountManagerId = extraReportParams.ClaimAccountManagerId,
                    ClaimManagerId = extraReportParams.ClaimManagerId,
                    ProductSupplierId = extraReportParams.ProductSupplierId,
                    ClaimStatusId = extraReportParams.ClaimStatusId,
                    IsPayStore = extraReportParams.IsPayStore,
                    IsClaimFromSupplier = extraReportParams.IsClaimFromSupplier,
                    ClaimSupplierStatusId = extraReportParams.ClaimSupplierStatusId,
                };

                var result = await Mediator.Send(command);

                var workSheet = package.Workbook.Worksheets.Add(result.RegionName);
                workSheet.Cells.LoadFromCollection(result.Data, true);

                var rowCount = workSheet.Dimension.Rows;
                if (rowCount > 0)
                {
                    rowCount = rowCount + 2;
                    if (result.ReportSubTotals.Count > 0)
                    {
                        foreach (var total in result.ReportSubTotals)
                        {
                            workSheet.Cells[rowCount, 1].Value = total.Name;
                            workSheet.Cells[rowCount, 2].Value = total.Value;
                            rowCount = rowCount + 1;
                        }
                    }
                }
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Extra Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }


    [HttpPost("related")]
    public async Task<IActionResult> GetRelatedReport(RelatedReportParams financeReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            //Related Summary
            var workSheetSummaryRelated = package.Workbook.Worksheets.Add("Summary-Related");
            var workSheetSummaryNonRelated = package.Workbook.Worksheets.Add("Summary-Non Related");

            foreach (var regionId in financeReportParams.EngageRegionIds)
            {
                var commandSummary = new GenerateClaimSummaryRelatedReportCommand
                {
                    //ClaimClassificationId = financeReportParams.ClaimClassificationId,
                    ClaimClassificationIds = financeReportParams.ClaimClassificationIds,
                    EngageRegionId = regionId,
                    ClaimPeriodId = financeReportParams.ClaimPeriodId,
                    //SupplierIds = financeReportParams.SupplierIds,
                    ToClaimPeriodId = financeReportParams.ToClaimPeriodId,
                    ClaimTypeIds = financeReportParams.ClaimTypeIds,
                    StoreId = financeReportParams.StoreId,
                    ClaimAccountManagerId = financeReportParams.ClaimAccountManagerId,
                    ClaimManagerId = financeReportParams.ClaimManagerId,
                    ProductSupplierId = financeReportParams.ProductSupplierId,
                };

                var resultSummary = await Mediator.Send(commandSummary);

                var r = workSheetSummaryRelated.Dimension;
                if (r != null)
                {
                    workSheetSummaryRelated.Cells[r.Rows + 2, 1].Value = resultSummary.RegionName + " - " + resultSummary.ClassificationName;
                }
                else
                {
                    workSheetSummaryRelated.Cells[1, 1].Value = resultSummary.RegionName + " - " + resultSummary.ClassificationName;
                }

                workSheetSummaryRelated.Cells["A" + (workSheetSummaryRelated.Dimension.Rows + 1)].LoadFromCollection(resultSummary.Data, false);

                //Non Related Summary
                var commandSummaryNoRelated = new GenerateClaimSummaryRelatedReportCommand
                {
                    //ClaimClassificationId = financeReportParams.ClaimClassificationId,
                    ClaimClassificationIds = financeReportParams.ClaimClassificationIds,
                    EngageRegionId = regionId,
                    ClaimPeriodId = financeReportParams.ClaimPeriodId,
                    //SupplierIds = financeReportParams.SupplierIds,
                    ToClaimPeriodId = financeReportParams.ToClaimPeriodId,
                    ClaimTypeIds = financeReportParams.ClaimTypeIds,
                    StoreId = financeReportParams.StoreId,
                    ClaimAccountManagerId = financeReportParams.ClaimAccountManagerId,
                    ClaimManagerId = financeReportParams.ClaimManagerId,
                    ProductSupplierId = financeReportParams.ProductSupplierId,
                    IsRelated = false,
                };

                var resultSummaryNonRelated = await Mediator.Send(commandSummaryNoRelated);

                var rnr = workSheetSummaryNonRelated.Dimension;
                if (rnr != null)
                {
                    workSheetSummaryNonRelated.Cells[rnr.Rows + 2, 1].Value = resultSummaryNonRelated.RegionName + " - " + resultSummaryNonRelated.ClassificationName;
                }
                else
                {
                    workSheetSummaryNonRelated.Cells[1, 1].Value = resultSummaryNonRelated.RegionName + " - " + resultSummaryNonRelated.ClassificationName;
                }

                workSheetSummaryNonRelated.Cells["A" + (workSheetSummaryNonRelated.Dimension.Rows + 1)].LoadFromCollection(resultSummaryNonRelated.Data, false);

                //Related
                var commandRelated = new GenerateClaimRelatedReportCommand
                {
                    //ClaimClassificationId = financeReportParams.ClaimClassificationId,
                    ClaimClassificationIds = financeReportParams.ClaimClassificationIds,
                    EngageRegionId = regionId,
                    ClaimPeriodId = financeReportParams.ClaimPeriodId,
                    SupplierId = financeReportParams.SupplierId,
                    ToClaimPeriodId = financeReportParams.ToClaimPeriodId,
                    ClaimTypeIds = financeReportParams.ClaimTypeIds,
                    StoreId = financeReportParams.StoreId,
                    ClaimAccountManagerId = financeReportParams.ClaimAccountManagerId,
                    ClaimManagerId = financeReportParams.ClaimManagerId,
                    ProductSupplierId = financeReportParams.ProductSupplierId,
                };

                var resultRelated = await Mediator.Send(commandRelated);

                var workSheet = package.Workbook.Worksheets.Add(resultRelated.RegionName);
                workSheet.Cells.LoadFromCollection(resultRelated.Data, true);

                var rowCount = workSheet.Dimension.Rows;
                if (rowCount > 0)
                {
                    rowCount = rowCount + 2;
                    if (resultRelated.ReportSubTotals.Count > 0)
                    {
                        foreach (var total in resultRelated.ReportSubTotals)
                        {
                            workSheet.Cells[rowCount, 1].Value = total.Name;
                            workSheet.Cells[rowCount, 2].Value = total.Value;
                            rowCount = rowCount + 1;
                        }
                    }
                }

                //Non Related
                var commandNonRelated = new GenerateClaimRelatedReportCommand
                {
                    //ClaimClassificationId = financeReportParams.ClaimClassificationId,
                    ClaimClassificationIds = financeReportParams.ClaimClassificationIds,
                    EngageRegionId = regionId,
                    ClaimPeriodId = financeReportParams.ClaimPeriodId,
                    SupplierId = financeReportParams.SupplierId,
                    ToClaimPeriodId = financeReportParams.ToClaimPeriodId,
                    ClaimTypeIds = financeReportParams.ClaimTypeIds,
                    StoreId = financeReportParams.StoreId,
                    ClaimAccountManagerId = financeReportParams.ClaimAccountManagerId,
                    ClaimManagerId = financeReportParams.ClaimManagerId,
                    ProductSupplierId = financeReportParams.ProductSupplierId,
                    IsRelated = false,
                };

                var resultNonRelated = await Mediator.Send(commandNonRelated);

                var workSheetNonRelated = package.Workbook.Worksheets.Add(resultNonRelated.RegionName);
                workSheetNonRelated.Cells.LoadFromCollection(resultNonRelated.Data, true);

                var rowCountNonRelated = workSheetNonRelated.Dimension.Rows;
                if (rowCountNonRelated > 0)
                {
                    rowCountNonRelated = rowCountNonRelated + 2;
                    if (resultNonRelated.ReportSubTotals.Count > 0)
                    {
                        foreach (var total in resultNonRelated.ReportSubTotals)
                        {
                            workSheetNonRelated.Cells[rowCountNonRelated, 1].Value = total.Name;
                            workSheetNonRelated.Cells[rowCountNonRelated, 2].Value = total.Value;
                            rowCountNonRelated = rowCountNonRelated + 1;
                        }
                    }
                }
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Related Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }
}
