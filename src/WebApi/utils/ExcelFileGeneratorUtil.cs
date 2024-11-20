using ClosedXML.Excel;
using Engage.Application.Services.EmployeeStoreCalendars.Queries;
using Engage.Application.Services.Orders.Models;
using Engage.Application.Services.ProductOrders.Queries;
using Engage.Application.Services.ProductWarehouseSummaries.Queries;
using Engage.Application.Services.SurveyInstances.Models;
using OfficeOpenXml;
using System.Drawing;

namespace Engage.WebApi.utils;

public static class ExcelFileGeneratorUtil
{
    public static MemoryStream GenerateExcelFileStream<T>(int dataColumnCount, object reportName, List<T> data, List<string> columnNames, string worksheetName)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add(worksheetName);
            //set column headings
            int colIndex = 1;
            foreach (var row in columnNames)
            {
                workSheet.Cells[1, colIndex].Value = row;
                colIndex++;
            }

            //load data
            workSheet.Cells["A2"].LoadFromCollection(data, false);

            package.Save();
        }

        stream.Position = 0;
        return stream;
    }

    public static async Task<MemoryStream> GenerateCompleteSurveyExcelStream(List<SurveyInstanceWebAllAnswersDto> surveyAnswers, EmployeeStoreCalendarBySurveyInstanceVm employeeStoreCalendar, DateTime reportDate)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new XLWorkbook())
        {
            IXLWorksheet workSheet = package.Worksheets.Add("Contact Sheet");

            workSheet.Column("B").Width = 44;
            workSheet.Column("C").Width = 44;
            workSheet.Row(1).Height = 49;
            workSheet.Row(2).Height = 36;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(employeeStoreCalendar.EngageLogo);
                var response = await client.GetAsync(employeeStoreCalendar.EngageLogo);
                var imageStream = await response.Content.ReadAsStreamAsync();
                var logo = workSheet.AddPicture(imageStream)
                    .MoveTo(workSheet.Cell("B1"), new Point(210, 15)).WithSize(190, 58);
            }

            #region Contact Report Details

            workSheet.Cells("B1:C2").Style.Fill.BackgroundColor = XLColor.Black;
            workSheet.Range("B2:C2").Merge();
            workSheet.MergedRanges.First(r => r.Contains("C2")).Value = "Store Contact Report";
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.FontColor = XLColor.White;
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.Bold = true;
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.FontSize = 28;

            workSheet.Cell("B4").Value = "Details";
            workSheet.Range("B4:B9").Style.Font.Bold = true;
            workSheet.Range("B4:B9").Style.Font.FontSize = 12;
            workSheet.Range("B4:C4").Style.Border.BottomBorder = XLBorderStyleValues.Thick;

            workSheet.Cell("B5").Value = "Store";
            workSheet.Cell("C5").Value = employeeStoreCalendar.StoreId.Name;

            workSheet.Cell("B6").Value = "Employee";
            workSheet.Cell("C6").Value = employeeStoreCalendar.EmployeeId.Name;

            workSheet.Cell("B7").Value = "Job Titles";
            workSheet.Cell("C7").Value = string.Join(", ", employeeStoreCalendar.JobTitles);

            workSheet.Cell("B8").Value = "Appointment Date:";
            workSheet.Cell("C8").Value = employeeStoreCalendar.CalendarDate;
            workSheet.Cell("C8").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            workSheet.Cell("B9").Value = "Contact Report Completed on:";
            workSheet.Cell("C9").Value = reportDate.ToShortDateString();
            workSheet.Cell("C9").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            workSheet.Row(11).Height = 24;
            workSheet.Cells("B11:C11").Style.Fill.BackgroundColor = XLColor.FromHtml("#C00000");
            workSheet.Range("B11:C11").Merge();
            workSheet.MergedRanges.First(r => r.Contains("B11")).Value = "Questions:";
            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Font.FontSize = 16;
            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Font.FontColor = XLColor.White;
            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            workSheet.MergedRanges.First(r => r.Contains("B11")).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            #endregion

            int rowIndex = 12; //start putting answers on row 12

            foreach (var answer in surveyAnswers)
            {
                workSheet.Row(rowIndex).Height = 24;
                if (answer.QuestionType != "Boolean")
                {
                    var currSheet = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();

                    currSheet.Value = answer.Question;
                    currSheet.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    currSheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    currSheet.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    currSheet.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    currSheet.Style.Fill.BackgroundColor = XLColor.FromHtml("#F2DCDB");
                    currSheet.Style.Font.Bold = true;
                    rowIndex++;
                }

                switch (answer.QuestionType)
                {
                    case "Checkbox":
                        workSheet.Range(rowIndex, 2, rowIndex, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        workSheet.Range(rowIndex, 2, rowIndex, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        workSheet.Range(rowIndex, 2, rowIndex, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        workSheet.Range(rowIndex, 2, rowIndex, 3).Style.Border.TopBorderColor = XLColor.Black;
                        foreach (var answerOption in answer.AnswerOptions)
                        {
                            workSheet.Row(rowIndex).Height = 24;
                            var answerCell = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();
                            answerCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            answerCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                            answerCell.Value = answerOption;
                            rowIndex++;
                        }
                        workSheet.Range(rowIndex, 2, rowIndex, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        break;
                    case "Photo":
                        var currentSheetPhoto = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();
                        currentSheetPhoto.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        currentSheetPhoto.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        currentSheetPhoto.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        currentSheetPhoto.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        currentSheetPhoto.Style.Border.TopBorderColor = XLColor.Black;

                        if (answer.FilePhotoAnswer != null)
                        {
                            foreach (var answerCell in answer.FilePhotoAnswer)
                            {
                                try
                                {
                                    using var client = new HttpClient();
                                    client.BaseAddress = new Uri(answerCell.Url);
                                    var response = await client.GetAsync(answerCell.Url);
                                    var imageStream = await response.Content.ReadAsStreamAsync();
#pragma warning disable CA1416 // Validate platform compatibility
                                    var img = Bitmap.FromStream(imageStream);
#pragma warning restore CA1416 // Validate platform compatibility

#pragma warning disable CA1416 // Validate platform compatibility
                                    int imageWidth = img.Width;
#pragma warning restore CA1416 // Validate platform compatibility
                                    double resizePercent = 1;
                                    //work out percent to resize image to fit in columns B & C
#pragma warning disable CA1416 // Validate platform compatibility
                                    if (img.Width > 600)
                                    {
                                        resizePercent = 600.0 / img.Width;
                                        imageWidth = (int)(img.Width * resizePercent);
                                    }
#pragma warning restore CA1416 // Validate platform compatibility

                                    // 7.31 is estimated pixel to column width image spread across 2 columns
                                    double bufferTest = (imageWidth / 7.31) / 2;
                                    // column B and C width = 44
                                    bufferTest = 44 - bufferTest;
                                    //estimated column width to column pixel width
                                    bufferTest = bufferTest * 8;

                                    imageStream.Position = 0;

                                    var image = workSheet.AddPicture(imageStream)
                                        .MoveTo(workSheet.Cell(rowIndex, 2), new Point((int)bufferTest, 15));

#pragma warning disable CA1416 // Validate platform compatibility
                                    image.WithSize(imageWidth, (int)(img.Height * resizePercent));
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
                                    workSheet.Row(rowIndex).Height = ((img.Height * 72 / 96)) * resizePercent + 20;
#pragma warning restore CA1416 // Validate platform compatibility

                                    rowIndex++;
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }

                        break;
                    case "Text":
                        workSheet.Row(rowIndex).Height = (int)(Math.Ceiling(((double)answer.Answer.Length) / 100) * 18 + 6);
                        var currentCellText = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();
                        currentCellText.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        currentCellText.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        currentCellText.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        currentCellText.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        currentCellText.Style.Border.TopBorderColor = XLColor.Black;
                        currentCellText.Style.Alignment.WrapText = true;

                        currentCellText.Value = answer.Answer;
                        currentCellText.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        currentCellText.Style.Border.TopBorderColor = XLColor.Black;
                        rowIndex++;
                        break;
                    case "Boolean":
                        workSheet.Row(rowIndex).Height = 24;
                        //answer is true or has no false reason
                        if (answer.QuestionFalseReason == "")
                        {
                            //question
                            var currentCell = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();
                            currentCell.Style.Font.Bold = true;
                            currentCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#F2DCDB");
                            currentCell.Style.Font.Bold = true;
                            currentCell.Value = answer.Question;
                            rowIndex++;
                            //answer
                            workSheet.Row(rowIndex).Height = 24;
                            currentCell = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();
                            currentCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            currentCell.Value = answer.Answer == "False" ? "No" : "Yes";

                        }//answer is false and has false reason 
                        else
                        {
                            var currentCell = workSheet.Cell(rowIndex, 2);
                            currentCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Alignment.WrapText = true;
                            currentCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#F2DCDB");
                            currentCell.Style.Font.Bold = true;
                            currentCell.Value = answer.Question;
                            workSheet.Row(rowIndex).Height = 35;

                            currentCell = workSheet.Cell(rowIndex, 3);
                            currentCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#F2DCDB");
                            currentCell.Style.Font.Bold = true;
                            currentCell.Value = "False Reason";
                            rowIndex++;

                            workSheet.Row(rowIndex).Height = 24;
                            currentCell = workSheet.Cell(rowIndex, 2);
                            currentCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            currentCell.Value = answer.Answer == "False" ? "No" : "Yes";

                            currentCell = workSheet.Cell(rowIndex, 3);
                            currentCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            currentCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            currentCell.Value = answer.QuestionFalseReason;
                        }
                        rowIndex++;
                        break;
                }
            }

            if (employeeStoreCalendar.SurveyInstanceNote != "")
            {
                workSheet.Row(rowIndex).Height = 26;
                var noteCell = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();

                noteCell.Style.Border.TopBorder = XLBorderStyleValues.Thick;
                noteCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                noteCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                noteCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                noteCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                noteCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                noteCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#F2DCDB");
                noteCell.Style.Font.Bold = true;
                noteCell.Value = "Contact Report Note";
                rowIndex++;

                workSheet.Row(rowIndex).Height = Math.Ceiling(((double)employeeStoreCalendar.SurveyInstanceNote.Length) / 100) * 20 + 6;
                noteCell = workSheet.Range(rowIndex, 2, rowIndex, 3).Merge();
                noteCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                noteCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                noteCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                noteCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                noteCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                noteCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                noteCell.Style.Alignment.WrapText = true;
                noteCell.Value = employeeStoreCalendar.SurveyInstanceNote;
            }

            workSheet.Range(12, 2, rowIndex, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            workSheet.Range(12, 2, rowIndex, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            package.SaveAs(stream);
        }

        stream.Position = 0;
        return stream;
    }

    public static async Task<MemoryStream> GenerateOrderSummeryExcelStream(OrderVM orderVm, string engageLogo, string RepName)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new XLWorkbook())
        {
            IXLWorksheet workSheet = package.Worksheets.Add("Order Summary");

            workSheet.Column("B").Width = 44;
            workSheet.Column("C").Width = 44;
            workSheet.Row(1).Height = 49;
            workSheet.Row(2).Height = 36;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(engageLogo);
                var response = await client.GetAsync(engageLogo);
                var imageStream = await response.Content.ReadAsStreamAsync();
                var logo = workSheet.AddPicture(imageStream)
                    .MoveTo(workSheet.Cell("C1"), new Point(125, 15)).WithSize(190, 58);
            }

            workSheet.Cells("B1:I2").Style.Fill.BackgroundColor = XLColor.Black;
            workSheet.Range("B2:I2").Merge();
            workSheet.MergedRanges.First(r => r.Contains("C2")).Value = "Order Confirmation";
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.FontColor = XLColor.White;
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.Bold = true;
            workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.FontSize = 28;

            workSheet.Cell("B4").Value = "Description";
            workSheet.Cell("C4").Value = "Value";
            workSheet.Range("B4:C4").Style.Font.Bold = true;
            workSheet.Range("B4:C4").Style.Font.FontSize = 12;
            workSheet.Range("B4:C4").Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            workSheet.Cell("B5").Value = "Store";
            workSheet.Cell("B5").Style.Font.Bold = true;
            workSheet.Cell("C5").Value = orderVm.StoreIdOption.Name;

            workSheet.Cell("B6").Value = "Distribution Center / Account";
            workSheet.Cell("B6").Style.Font.Bold = true;
            workSheet.Cell("C6").Value = orderVm.DistributionCenterIdOption.Name;

            workSheet.Cell("B7").Value = "Engage Departments";
            workSheet.Cell("B7").Style.Font.Bold = true;
            int cellIndex = 7;
            foreach (var department in orderVm.Order.EngageDepartments)
            {
                workSheet.Cell("C" + cellIndex).Value = department.Name;
                cellIndex++;
            }

            workSheet.Cell("B" + cellIndex).Value = "Order Dates";
            workSheet.Cell("B" + cellIndex).Style.Font.Bold = true;
            workSheet.Cell("C" + cellIndex).Value = orderVm.Order.OrderDate.ToShortDateString();
            workSheet.Cell("C" + cellIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            cellIndex++;

            workSheet.Cell("B" + cellIndex).Value = "Delivery Date";
            workSheet.Cell("B" + cellIndex).Style.Font.Bold = true;
            workSheet.Cell("C" + cellIndex).Value = orderVm.Order.DeliveryDate.HasValue
                                                    ? orderVm.Order.DeliveryDate.Value.ToShortDateString()
                                                    : "";
            workSheet.Cell("C" + cellIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            cellIndex++;

            workSheet.Cell("B" + cellIndex).Value = "Reference";
            workSheet.Cell("B" + cellIndex).Style.Font.Bold = true;
            workSheet.Cell("C" + cellIndex).Value = orderVm.Order.OrderReference;
            cellIndex++;

            workSheet.Cell("B" + cellIndex).Value = "Order Placed By:";
            workSheet.Cell("B" + cellIndex).Style.Font.Bold = true;
            workSheet.Cell("C" + cellIndex).Value = RepName;

            cellIndex += 2;

            //order lines
            foreach (var orderSku in orderVm.OrderSkus.ProductSkus)
            {
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Merge();
                workSheet.MergedRanges.First(r => r.Contains($"C{cellIndex}")).Value = $"Order Lines - {orderSku.Key}";
                workSheet.MergedRanges.First(r => r.Contains($"C{cellIndex}")).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Font.FontColor = XLColor.White;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Fill.BackgroundColor = XLColor.FromHtml("#C00000");
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Font.FontSize = 16;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                workSheet.Cell($"B{cellIndex}").Style.Font.FontSize = 16;
                cellIndex++;

                workSheet.Cell($"B{cellIndex}").Value = "Product Code";
                workSheet.Cell($"C{cellIndex}").Value = "Name";
                workSheet.Cell($"D{cellIndex}").Value = "Size";
                workSheet.Cell($"E{cellIndex}").Value = "Unit";
                workSheet.Cell($"F{cellIndex}").Value = "PackSize";
                workSheet.Cell($"G{cellIndex}").Value = "Qty Type";
                workSheet.Cell($"H{cellIndex}").Value = "Qty";
                workSheet.Cell($"I{cellIndex}").Value = "Delivery Date";
                workSheet.Cell($"I{cellIndex}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Font.Bold = true;
                cellIndex++;

                int orderQuantity = 0;

                foreach (var order in orderSku.Value)
                {
                    workSheet.Cell($"B{cellIndex}").Value = order.DCProductCode;
                    workSheet.Cell($"C{cellIndex}").Value = order.DCProductName;
                    workSheet.Cell($"D{cellIndex}").Value = order.DCProductSize;
                    workSheet.Cell($"E{cellIndex}").Value = order.DCProductUnitType;
                    workSheet.Cell($"F{cellIndex}").Value = order.DCProductPackSize;
                    workSheet.Cell($"G{cellIndex}").Value = order.OrderQuantityTypeName;
                    workSheet.Cell($"H{cellIndex}").Value = order.Quantity;
                    workSheet.Cell($"I{cellIndex}").Value = order.DeliveryDate.HasValue ? order.DeliveryDate.Value.ToShortDateString() : "";
                    workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    workSheet.Cell($"B{cellIndex}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    workSheet.Cell($"I{cellIndex}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    orderQuantity += order.Quantity;
                    cellIndex++;
                }

                workSheet.Cell($"C{cellIndex}").Value = "Total";
                workSheet.Cell($"H{cellIndex}").Value = orderQuantity;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Fill.BackgroundColor = XLColor.LightGray;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Font.Bold = true;
                workSheet.Cell($"I{cellIndex}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                workSheet.Range($"B{cellIndex}:I{cellIndex}").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                cellIndex += 2;
            }
            workSheet.Columns().AdjustToContents();
            workSheet.Row(1).Height = 48;
            workSheet.Row(2).Height = 50;
            package.SaveAs(stream);

        }
        stream.Position = 0;
        return stream;
    }

    public static async Task<MemoryStream> GenerateStockOnHandSummary(List<ProductWarehouseSummaryStockReportDto> stockDto)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new XLWorkbook())
        {
            foreach (var warehouse in stockDto)
            {
                string worksheetName = warehouse.ProductWarehouseName;
                IXLWorksheet workSheet = package.Worksheets.Add(worksheetName);

                workSheet.Column("B").Width = 44;
                workSheet.Column("C").Width = 44;
                workSheet.Row(1).Height = 49;
                workSheet.Row(2).Height = 36;

                #region EngageLogo
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(warehouse.EngageLogo);
                    var response = await client.GetAsync(warehouse.EngageLogo);
                    var imageStream = await response.Content.ReadAsStreamAsync();
                    var logo = workSheet.AddPicture(imageStream)
                        .MoveTo(workSheet.Cell("D1"), new Point(125, 15)).WithSize(190, 58);
                }

                int rowCount = 5;

                workSheet.Cells("B1:I2").Style.Fill.BackgroundColor = XLColor.Black;
                workSheet.Range("B2:I2").Merge();
                workSheet.MergedRanges.First(r => r.Contains("C2")).Value = "Stock Report - " + worksheetName;
                workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.FontColor = XLColor.White;
                workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.Bold = true;
                workSheet.MergedRanges.First(r => r.Contains("C2")).Style.Font.FontSize = 24;
                #endregion

                #region TableHeading
                workSheet.Cell("D4").Value = "Uniform Name";
                workSheet.Cell("D4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.Cell("D4").Style.Fill.BackgroundColor = XLColor.FromHtml("#C00000");
                workSheet.Cell("D4").Style.Font.FontColor = XLColor.White;

                workSheet.Cell("D4").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("D4").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("D4").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("D4").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                workSheet.Cell("E4").Value = "Current Stock";
                workSheet.Cell("E4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.Cell("E4").Style.Fill.BackgroundColor = XLColor.FromHtml("#C00000");
                workSheet.Cell("E4").Style.Font.FontColor = XLColor.White;

                workSheet.Cell("E4").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("E4").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("E4").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("E4").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                #endregion

                //start adding data
                var masterGrouped = warehouse.Products.GroupBy(e => e.ProductMasterId).ToDictionary(k => k.Key, k => k.ToList()).OrderBy(e => e.Key);
                foreach (var product in masterGrouped)
                {
                    //create product Master
                    workSheet.Cell("D" + rowCount).Value = product.Value[0].ProductMasterName;
                    workSheet.Cell("D" + rowCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    workSheet.Cell("D" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    workSheet.Cell("D" + rowCount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    workSheet.Cell("D" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    workSheet.Cell("D" + rowCount).Style.Font.FontSize = 16;
                    workSheet.Cell("D" + rowCount).Style.Font.Bold = true;

                    workSheet.Cell("E" + rowCount).Value = product.Value.Select(e => e.CurrentStock).Sum();
                    workSheet.Cell("E" + rowCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    workSheet.Cell("E" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    workSheet.Cell("E" + rowCount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    workSheet.Cell("E" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                    workSheet.Cells($"D{rowCount}:E{rowCount}").Style.Fill.BackgroundColor = XLColor.LightGray;
                    rowCount++;

                    foreach (var item in product.Value)
                    {
                        //create each product
                        workSheet.Cell("D" + rowCount).Value = item.ProductName;
                        workSheet.Cell("D" + rowCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        workSheet.Cell("D" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        workSheet.Cell("D" + rowCount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        workSheet.Cell("E" + rowCount).Value = item.CurrentStock;
                        workSheet.Cell("E" + rowCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        workSheet.Cell("E" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        workSheet.Cell("E" + rowCount).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        rowCount++;
                    }

                    workSheet.Cells("D" + rowCount + ":E" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                }

                workSheet.Cell("D" + rowCount).Value = "Warehouse Total";
                workSheet.Cell("D" + rowCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                workSheet.Cell("D" + rowCount).Style.Font.Bold = true;
                workSheet.Cell("D" + rowCount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("D" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                workSheet.Cell("E" + rowCount).Value = warehouse.Products.Select(e => e.CurrentStock).Sum();
                workSheet.Cell("E" + rowCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                workSheet.Cell("E" + rowCount).Style.Font.Bold = true;
                workSheet.Cell("E" + rowCount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                workSheet.Cell("E" + rowCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                workSheet.Cells($"D{rowCount}:E{rowCount}").Style.Fill.BackgroundColor = XLColor.LightGray;

                workSheet.Columns().AdjustToContents();
            }
            package.SaveAs(stream);
        }

        stream.Position = 0;
        return stream;
    }

    public static MemoryStream GenerateMutliPageExcelStream<T>(Dictionary<string, List<T>> data, List<String> columnNames)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            if (data.Count == 0)
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                int colIndex = 1;
                foreach (var row in columnNames)
                {
                    workSheet.Cells[1, colIndex].Value = row;
                    colIndex++;
                }
            }
            foreach (var worksheet in data)
            {
                var workSheet = package.Workbook.Worksheets.Add(worksheet.Key);
                int colIndex = 1;
                foreach (var row in columnNames)
                {
                    workSheet.Cells[1, colIndex].Value = row;
                    colIndex++;
                }
                workSheet.Cells["A2"].LoadFromCollection(worksheet.Value, false);
            }
            package.Save();
        }

        stream.Position = 0;
        return stream;
    }

    public static MemoryStream GeneratePurchaseOrderStream(List<ProductOrderReportDto> reportDto)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var warehouse in reportDto)
            {
                string worksheetName = warehouse.ProductWarehouseName;
                var workSheet = package.Workbook.Worksheets.Add(worksheetName);

                int rowNumber = 3;

                workSheet.Cells[1, rowNumber].Value = "Order Id";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Order Number";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Order Date";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Status";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Type";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Product Code";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Product";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Warehouse";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Warehouse Out";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Quantity";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Amount";
                rowNumber++;
                workSheet.Cells[1, rowNumber].Value = "Note";

                if (warehouse.Lines.Count > 0)
                {
                    workSheet.Cells["C2"].LoadFromCollection(warehouse.Lines, false);
                }

                rowNumber = warehouse.Lines.Count + 3; //account for heading and one row gap

                workSheet.Cells[rowNumber, 1].Value = "Total Quantity:";
                workSheet.Cells[rowNumber, 2].Value = warehouse.Lines.Select(e => e.Quantity).ToList().Sum();
                rowNumber++;

                workSheet.Cells[rowNumber, 1].Value = "Total:";
                workSheet.Cells[rowNumber, 2].Value = warehouse.Lines.Select(e => (decimal)e.Quantity * e.Amount).ToList().Sum();
                rowNumber++;
            }
            package.SaveAs(stream);
        }

        stream.Position = 0;
        return stream;
    }
}
