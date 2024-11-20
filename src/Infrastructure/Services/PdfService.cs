using Engage.Application.Services.EmployeeStoreCalendars.Queries;
using Engage.Application.Services.Orders.Models;
using Engage.Application.Services.OrderStagings.Queries;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Engage.Infrastructure.Services;

public class PdfService : IPdfService
{
    private static void ComposeHeader(IContainer container, Stream imageStream, string title)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.White);

        container.Background(Colors.Black)
                 .Column(column =>
        {
            column.Item()
                  .AlignCenter()
                  .Height(2, Unit.Centimetre)
                  .Image(imageStream)
                    .FitHeight();

            column.Item()
                  .AlignCenter()
                  .PaddingBottom(2, Unit.Millimetre)
                  .Text(title).Style(titleStyle);
        });
    }

    private static void ComposeFooter(IContainer container)
    {
        var pageNumberStyle = TextStyle.Default.FontColor(Colors.Black).Italic();

        container.Row(row =>
        {
            row.ConstantItem(4, Unit.Centimetre)
               .AlignLeft()
               .Padding(8)
               .Text(x =>
               {
                   x.Span("Page ");
                   x.CurrentPageNumber();
                   x.DefaultTextStyle(pageNumberStyle);
               });
        });
    }
    public async Task<MemoryStream> GenerateOrderSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : OrderSummaryPdfVm
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(pdfData.HeaderImageURL);
        var response = await client.GetAsync(pdfData.HeaderImageURL, cancellationToken);
        var imageStream = await response.Content.ReadAsStreamAsync(cancellationToken);



        var document = Document.Create(container =>
            container.Page(page =>
            {
                page.Size(PageSizes.A3);

                page.Header()
                    .Element(e => ComposeHeader(e, imageStream, "Order confirmation"));

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .PaddingHorizontal(1f, Unit.Centimetre)
                    .AlignCenter()
                    .Column(column =>
                    {
                        //Generate order details table
                        column.Item()
                              .PaddingBottom(1, Unit.Centimetre)
                              .PaddingHorizontal(3, Unit.Centimetre)
                              .AlignCenter()
                              .Table(table =>
                              {
                                  //step1
                                  table.ColumnsDefinition(columns =>
                                  {
                                      columns.ConstantColumn(7, Unit.Centimetre);
                                      columns.RelativeColumn();
                                  });
                                  //step2
                                  table.Header(header =>
                                  {
                                      header.Cell().Element(CellStyle).Text(" Description");
                                      header.Cell().Element(CellStyle).Text(" Value");

                                      static IContainer CellStyle(IContainer container)
                                      {
                                          return container.DefaultTextStyle(x => x.SemiBold().FontSize(16))
                                                          .BorderBottom(1, Unit.Point)
                                                          .BorderColor(Colors.Black);
                                      }
                                  });
                                  //step3
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text("Store");
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.StoreName);

                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text("Distribution Center/Account");
                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text(pdfData.Data.DistributionCenterName);

                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text("Order Dates");
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text(pdfData.Data.OrderDate);

                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text("Delivery Dates");
                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text(pdfData.Data.DeliveryDate);

                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text("Reference");
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text(pdfData.Data.OrderReference);

                                  table.Cell().Element(DataCellStyle).Background(Colors.White).BorderBottom(0.5f, Unit.Millimetre).Text("Order Placed By");
                                  table.Cell().Element(DataCellStyle).Background(Colors.White).BorderBottom(0.5f, Unit.Millimetre).Text(pdfData.Data.PlacedBy);

                                  static IContainer DataCellStyle(IContainer container)
                                  {
                                      return container.DefaultTextStyle(x => x.FontSize(12))
                                                      .BorderBottom(1, Unit.Point)
                                                      .BorderColor(Colors.Black);
                                  }
                              });

                        //Generate order Skus table
                        foreach (var orderSku in pdfData.Data.OrderSkus.ProductSkus)
                        {
                            column.Item()
                                  .PaddingTop(1, Unit.Centimetre)
                                  .Column(orderSkuColumn =>
                                  {

                                      orderSkuColumn.Item()
                                                    .PaddingVertical(0)
                                                    .Background("#C00000")
                                                    .Table(table =>
                                                    {
                                                        table.ColumnsDefinition(column =>
                                                        {
                                                            column.RelativeColumn();
                                                        });
                                                        table.Header(header =>
                                                        {
                                                            header.Cell()
                                                                  .AlignCenter()
                                                                  .DefaultTextStyle(x => x.FontSize(16).SemiBold().FontColor(Colors.White))
                                                                  .Text($"Order Lines - {orderSku.Key}");
                                                        });
                                                    });

                                      orderSkuColumn.Item()
                                                    .PaddingHorizontal(0)
                                                    .Table(table =>
                                                    {
                                                        table.ColumnsDefinition(column =>
                                                        {
                                                            column.ConstantColumn(3, Unit.Centimetre);
                                                            column.ConstantColumn(11.5f, Unit.Centimetre);
                                                            column.RelativeColumn();
                                                            column.RelativeColumn();
                                                            column.RelativeColumn();
                                                            column.RelativeColumn();
                                                            column.RelativeColumn();
                                                            column.ConstantColumn(3, Unit.Centimetre);
                                                        });

                                                        var skuCount = 1;
                                                        table.Header(header =>
                                                        {
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Product Code");
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Name");
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Size");
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Unit");
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Pack Size");
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Qty Type");
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Qty");
                                                            header.Cell().Element(CellStyle).AlignLeft().Text("Delivery Date");

                                                            IContainer CellStyle(IContainer container)
                                                            {
                                                                if (skuCount % 2 == 0)
                                                                {
                                                                    skuCount++;
                                                                    return container.DefaultTextStyle(x => x.SemiBold())
                                                                                .BorderBottom(0.5f, Unit.Millimetre)
                                                                                .Background(Colors.Grey.Lighten4)
                                                                                .PaddingHorizontal(5)
                                                                                .AlignMiddle()
                                                                                .BorderColor(Colors.Black);
                                                                }
                                                                else
                                                                {
                                                                    skuCount++;
                                                                    return container.DefaultTextStyle(x => x.SemiBold())
                                                                                .BorderBottom(0.5f, Unit.Millimetre)
                                                                                .Background(Colors.White)
                                                                                .PaddingHorizontal(5)
                                                                                .AlignMiddle()
                                                                                .BorderColor(Colors.Black);
                                                                }
                                                            }
                                                        });

                                                        var orderQuantity = 0;
                                                        skuCount = 1;
                                                        foreach (var order in orderSku.Value)
                                                        {
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.DCProductCode);
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.DCProductName);
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.DCProductSize.ToString());
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.DCProductUnitType);
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.DCProductPackSize.ToString());
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.OrderQuantityTypeName);
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.Quantity.ToString());
                                                            table.Cell().Element(CellStyle).AlignLeft().Text(order.DeliveryDate.HasValue ? order.DeliveryDate.Value.ToShortDateString() : "");

                                                            orderQuantity += order.Quantity;

                                                            IContainer CellStyle(IContainer container)
                                                            {
                                                                if (skuCount % 2 == 0)
                                                                {
                                                                    skuCount++;
                                                                    return container.DefaultTextStyle(x => x.FontSize(10))
                                                                                .Background(Colors.Grey.Lighten4)
                                                                                .PaddingHorizontal(5)
                                                                                .PaddingVertical(2);
                                                                }
                                                                else
                                                                {
                                                                    skuCount++;
                                                                    return container.DefaultTextStyle(x => x.FontSize(10))
                                                                                .Background(Colors.White)
                                                                                .PaddingHorizontal(5)
                                                                                .PaddingVertical(2);
                                                                }
                                                            }
                                                        }
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("");
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text(" Total").SemiBold();
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("");
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("");
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("");
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("");
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text($" {orderQuantity.ToString()}");
                                                        table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("");

                                                    });
                                  });

                        }
                    });

                page.Footer()
                    .PaddingHorizontal(1f, Unit.Centimetre)
                    .Element(ComposeFooter);

            }));

        MemoryStream stream = new(document.GeneratePdf());

        return stream;
    }

    public async Task<MemoryStream> GenerateOrderStagingSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : OrderStagingPdfDto
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(pdfData.HeaderImageURL);
        var response = await client.GetAsync(pdfData.HeaderImageURL, cancellationToken);
        var imageStream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var document = Document.Create(container =>
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Header()
                    .Element(e => ComposeHeader(e, imageStream, "Order confirmation"));

                page.Content()
                   .PaddingVertical(1, Unit.Centimetre)
                   .PaddingHorizontal(1f, Unit.Centimetre)
                   .AlignCenter()
                   .Column(column =>
                   {
                       //generate order details
                       column.Item()
                              .PaddingBottom(1, Unit.Centimetre)
                              .PaddingHorizontal(3, Unit.Centimetre)
                              .AlignCenter()
                              .Table(table =>
                              {
                                  //step1
                                  table.ColumnsDefinition(columns =>
                                  {
                                      columns.ConstantColumn(7, Unit.Centimetre);
                                      columns.RelativeColumn();
                                  });
                                  //step2
                                  table.Header(header =>
                                  {
                                      header.Cell().Element(CellStyle).Text(" Description");
                                      header.Cell().Element(CellStyle).Text(" Value");

                                      static IContainer CellStyle(IContainer container)
                                      {
                                          return container.DefaultTextStyle(x => x.SemiBold().FontSize(16))
                                                          .BorderBottom(1, Unit.Point)
                                                          .BorderColor(Colors.Black);
                                      }
                                  });
                                  //step3
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text("Store");
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.StoreName);

                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text("Region");
                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text(pdfData.Data.Region);

                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text("Account Number");
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text(pdfData.Data.AccountNumber);

                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text("Contact Name");
                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text(pdfData.Data.OrderContactName);

                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text("Reference");
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text(pdfData.Data.Reference);

                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text("Order Date");
                                  table.Cell().Element(DataCellStyle).Background(Colors.White).Text(pdfData.Data.OrderDate);

                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text("VAT Number");
                                  table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).Text(pdfData.Data.VatNumber);

                                  static IContainer DataCellStyle(IContainer container)
                                  {
                                      return container.DefaultTextStyle(x => x.FontSize(12))
                                                      .BorderBottom(1, Unit.Point)
                                                      .BorderColor(Colors.Black);
                                  }
                              });

                       //Generate order Skus table
                       foreach (var orderSku in pdfData.Data.Skus)
                       {
                           column.Item()
                                 .PaddingTop(1, Unit.Centimetre)
                                 .Column(orderSkuColumn =>
                                 {

                                     orderSkuColumn.Item()
                                                   .PaddingVertical(0)
                                                   .Background("#C00000")
                                                   .Table(table =>
                                                   {
                                                       table.ColumnsDefinition(column =>
                                                       {
                                                           column.RelativeColumn();
                                                       });
                                                       table.Header(header =>
                                                       {
                                                           header.Cell()
                                                                 .AlignCenter()
                                                                 .DefaultTextStyle(x => x.FontSize(16).SemiBold().FontColor(Colors.White))
                                                                 .Text($"Order Lines - {orderSku.Key}");
                                                       });
                                                   });

                                     orderSkuColumn.Item()
                                                   .PaddingHorizontal(0)
                                                   .Table(table =>
                                                   {
                                                       table.ColumnsDefinition(column =>
                                                       {
                                                           column.ConstantColumn(3.4f, Unit.Centimetre);
                                                           column.RelativeColumn();
                                                           column.ConstantColumn(3, Unit.Centimetre);
                                                       });

                                                       var skuCount = 1;
                                                       table.Header(header =>
                                                       {
                                                           header.Cell().Element(CellStyle).AlignLeft().Text("Barcode");
                                                           header.Cell().Element(CellStyle).AlignLeft().Text("Name");
                                                           header.Cell().Element(CellStyle).AlignLeft().Text("Quantity");

                                                           IContainer CellStyle(IContainer container)
                                                           {
                                                               if (skuCount % 2 == 0)
                                                               {
                                                                   skuCount++;
                                                                   return container.DefaultTextStyle(x => x.SemiBold())
                                                                               .BorderBottom(0.5f, Unit.Millimetre)
                                                                               .Background(Colors.Grey.Lighten4)
                                                                               .PaddingHorizontal(5)
                                                                               .AlignMiddle()
                                                                               .BorderColor(Colors.Black);
                                                               }
                                                               else
                                                               {
                                                                   skuCount++;
                                                                   return container.DefaultTextStyle(x => x.SemiBold())
                                                                               .BorderBottom(0.5f, Unit.Millimetre)
                                                                               .Background(Colors.White)
                                                                               .PaddingHorizontal(5)
                                                                               .AlignMiddle()
                                                                               .BorderColor(Colors.Black);
                                                               }
                                                           }
                                                       });

                                                       var orderQuantity = 0;
                                                       skuCount = 1;
                                                       foreach (var order in orderSku.Value)
                                                       {
                                                           table.Cell().Element(CellStyle).AlignLeft().Text(order.Barcode);
                                                           table.Cell().Element(CellStyle).AlignLeft().Text(order.ProductName);
                                                           table.Cell().Element(CellStyle).AlignLeft().Text(order.Quantity.ToString());

                                                           orderQuantity += order.Quantity;

                                                           IContainer CellStyle(IContainer container)
                                                           {
                                                               if (skuCount % 2 == 0)
                                                               {
                                                                   skuCount++;
                                                                   return container.DefaultTextStyle(x => x.FontSize(10))
                                                                               .Background(Colors.Grey.Lighten4)
                                                                               .PaddingHorizontal(5)
                                                                               .PaddingVertical(2);
                                                               }
                                                               else
                                                               {
                                                                   skuCount++;
                                                                   return container.DefaultTextStyle(x => x.FontSize(10))
                                                                               .Background(Colors.White)
                                                                               .PaddingHorizontal(5)
                                                                               .PaddingVertical(2);
                                                               }
                                                           }
                                                       }
                                                       table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text(" Total").SemiBold();
                                                       table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("");
                                                       table.Cell().Background(Colors.Grey.Lighten2).BorderBottom(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text($" {orderQuantity}");

                                                   });
                                 });

                       }
                   });

                page.Footer()
                    .PaddingHorizontal(1f, Unit.Centimetre)
                    .Element(ComposeFooter);

            }));

        MemoryStream stream = new(document.GeneratePdf());

        return stream;
    }

    public async Task<MemoryStream> GenerateContactReportSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : EmployeeStoreCalendarGeneratePdfReportDto
    {
        //using var client = new HttpClient();
        //client.BaseAddress = new Uri(pdfData.HeaderImageURL);
        //var response = await client.GetAsync(pdfData.HeaderImageURL, cancellationToken);
        //var imageStream = await response.Content.ReadAsStreamAsync(cancellationToken);

        Dictionary<int, List<Stream>> photoAnswers = new();
        foreach (var photoAnswer in pdfData.Data.Answers.Where(e => e.QuestionType == "Photo"))
        {
            List<Stream> photoStreamList = new();
            if (photoAnswer.FilePhotoAnswer != null)
            {
                foreach (var photo in photoAnswer.FilePhotoAnswer)
                {
                    try
                    {
                        using var photoClient = new HttpClient();
                        photoClient.BaseAddress = new Uri(photo.Url);
                        var photoResponse = await photoClient.GetAsync(photo.Url, cancellationToken);
                        photoStreamList.Add(await photoResponse.Content.ReadAsStreamAsync(cancellationToken));
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            photoAnswers.Add(photoAnswer.QuestionId, photoStreamList);
        }

        var document = Document.Create(container =>
        container.Page(page =>
        {
            page.Size(PageSizes.A4);

            page.Header()
                .Element(e => ComposeHeader(e, pdfData.HeaderStream, "Store Contact Report"));

            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .PaddingHorizontal(1, Unit.Centimetre)
                .AlignCenter()
                .Column(column =>
                {
                    column.Item()
                          .AlignCenter()
                          .PaddingHorizontal(0.5f, Unit.Centimetre)
                          .Table(table =>
                          {
                              //step 1
                              table.ColumnsDefinition(columns =>
                              {
                                  columns.RelativeColumn();
                                  columns.RelativeColumn();
                              });

                              //step 2
                              table.Header(header =>
                              {
                                  header.Cell().Element(CellStyle).AlignCenter().Text("Details");
                                  header.Cell().Element(CellStyle).Text("");

                                  static IContainer CellStyle(IContainer container)
                                  {
                                      return container.DefaultTextStyle(x => x.SemiBold().FontSize(16))
                                                      .BorderBottom(1, Unit.Point)
                                                      .BorderColor(Colors.Black);
                                  }
                              });

                              //step 3
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Store");
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.StoreName);

                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Employee");
                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderTop(0.5f, Unit.Millimetre).PaddingLeft(1, Unit.Point).Text(pdfData.Data.EmployeeName);

                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Job Title");
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(string.Join(", ", pdfData.Data.JobTitles));

                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Employee");
                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.EmployeeName);

                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Appointment Date");
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.CalendarDate);

                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Contact Report Completed on");
                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.SurveyInstanceCompletionDate);

                              static IContainer DataCellStyle(IContainer container)
                              {
                                  return container.DefaultTextStyle(x => x.SemiBold().FontSize(14))
                                                      .BorderBottom(1, Unit.Point)
                                                      .BorderColor(Colors.Black);
                              }
                          });

                    column.Item()
                          .PaddingTop(1, Unit.Centimetre)
                          .AlignCenter()
                          .Table(table =>
                          {
                              //Step 1
                              table.ColumnsDefinition(columns =>
                              {
                                  columns.RelativeColumn();
                              });

                              //step 2
                              table.Header(header =>
                              {
                                  header.Cell()
                                        .DefaultTextStyle(x => x.SemiBold().FontSize(16).FontColor(Colors.White))
                                        .Background(Colors.Red.Darken1)
                                        .BorderBottom(2, Unit.Point)
                                        .AlignCenter()
                                        .Text("Questions");
                              });

                              int rowCounter = 0;
                              //step 3
                              foreach (var question in pdfData.Data.Answers)
                              {
                                  if (question.QuestionType == "Text")
                                  {
                                      table.Cell().Element(DataCellStyle).Text(question.Question);
                                      rowCounter++;
                                      table.Cell().Element(DataCellStyle).Text(question.Answer);
                                      rowCounter++;
                                  }
                                  else if (question.QuestionType == "Boolean")
                                  {
                                      table.Cell().Element(DataCellStyle).Text($"{question.Question}");
                                      rowCounter++;
                                      table.Cell().Element(DataCellStyle).Text(question.Answer == "False" ? "No" : "True");
                                      rowCounter++;
                                  }
                                  else if (question.QuestionType == "Photo")
                                  {
                                      table.Cell().Element(DataCellStyle).Text(question.Question);
                                      rowCounter++;
                                      table.Cell().Element(DataCellStyle).Column(photCol =>
                                      {
                                          var photos = photoAnswers.Where(e => e.Key == question.QuestionId).Select(e => e.Value).FirstOrDefault();
                                          foreach (var item in photos)
                                          {
                                              photCol.Item()
                                                 .AlignCenter()
                                                 .PaddingBottom(3, Unit.Point)
                                                 .Height(5, Unit.Centimetre)
                                                 .Image(item)
                                                 .FitHeight();
                                          }
                                      });
                                      rowCounter++;
                                  }
                                  else if (question.QuestionType == "Checkbox")
                                  {
                                      table.Cell().Element(DataCellStyle).Text(question.Question);
                                      rowCounter++;
                                      foreach (var answer in question.AnswerOptions)
                                      {
                                          table.Cell().Element(DataCellStyle).Text(answer);
                                      }
                                      rowCounter++;
                                  }
                              }

                              IContainer DataCellStyle(IContainer container)
                              {
                                  return container.DefaultTextStyle(x => x.FontSize(12))
                                           .BorderBottom(1, Unit.Point)
                                           .BorderHorizontal(1, Unit.Point)
                                           .Background(rowCounter % 2 == 0 ? Colors.Red.Lighten5 : Colors.White)
                                           .PaddingVertical(1, Unit.Point)
                                           .AlignCenter();
                              }
                          });

                    if (pdfData.Data.Note.IsNotNullOrEmpty())
                    {
                        column.Item()
                              .PaddingTop(1, Unit.Centimetre)
                              .AlignCenter()
                              .Table(table =>
                              {
                                  table.ColumnsDefinition(column =>
                                  {
                                      column.RelativeColumn();
                                  });

                                  table.Header(header =>
                                  {
                                      header.Cell()
                                            .DefaultTextStyle(x => x.SemiBold().FontSize(16).FontColor(Colors.White))
                                            .Background(Colors.Red.Darken1)
                                            .BorderBottom(2, Unit.Point)
                                            .AlignCenter()
                                            .Text("Contact Report Note");
                                  });

                                  table.Cell()
                                       .AlignCenter()
                                       .PaddingVertical(3, Unit.Point)
                                       .DefaultTextStyle(x => x.FontSize(12))
                                       .BorderBottom(1, Unit.Point)
                                       .Text(pdfData.Data.Note);
                              });
                    }
                });

            page.Footer()
                    .PaddingHorizontal(1f, Unit.Centimetre)
                    .Element(ComposeFooter);
        }));

        MemoryStream stream = new(document.GeneratePdf());

        return stream;
    }

    public async Task<MemoryStream> GenerateSurveyFormContactReportSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : EmployeeStoreCalendarGenerateSurveyFormPdfReportDto
    {
        Dictionary<int, List<Stream>> photoAnswers = new();
        foreach (var photoAnswer in pdfData.Data.SurveySubmission.SelectMany(e => e.Answers.Where(f => f.QuestionType == "Photo").ToList()))
        {
            List<Stream> photoStreamList = new();
            if (photoAnswer.Files != null)
            {
                foreach (var photo in photoAnswer.Files)
                {
                    try
                    {
                        //using var photoClient = new HttpClient();
                        //photoClient.BaseAddress = new Uri(photo.Url);
                        //var photoResponse = await photoClient.GetAsync(photo.Url, cancellationToken);
                        var response = await pdfData.HttpClient.GetAsync(photo.Url, cancellationToken);
                        if (response != null && response.IsSuccessStatusCode)
                        {
                            photoStreamList.Add(await response.Content.ReadAsStreamAsync(cancellationToken));
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            photoAnswers.Add(photoAnswer.QuestionId, photoStreamList);
        }

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Header()
                    .Element(e => ComposeHeader(e, pdfData.HeaderStream, $"Contact Report"));

                page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .PaddingHorizontal(1, Unit.Centimetre)
                .AlignCenter()
                .Column(column =>
                {
                    var counter = 0;
                    foreach (var survey in pdfData.Data.SurveySubmission)
                    {
                        counter++;
                        column.Item()
                          .AlignCenter()
                          .PaddingHorizontal(0.5f, Unit.Centimetre)
                          .Table(table =>
                          {
                              //step 1
                              table.ColumnsDefinition(columns =>
                              {
                                  columns.RelativeColumn();
                                  columns.RelativeColumn();
                              });

                              //step 2
                              table.Header(header =>
                              {
                                  header.Cell().Element(CellStyle).AlignCenter().Text("Details");
                                  header.Cell().Element(CellStyle).Text("");

                                  static IContainer CellStyle(IContainer container)
                                  {
                                      return container.DefaultTextStyle(x => x.SemiBold().FontSize(16))
                                                      .BorderBottom(1, Unit.Point)
                                                      .BorderColor(Colors.Black);
                                  }
                              });

                              //step 3
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Store");
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.StoreName);

                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Employee");
                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderTop(0.5f, Unit.Millimetre).PaddingLeft(1, Unit.Point).Text(pdfData.Data.EmployeeName);

                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Job Title");
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(string.Join(", ", pdfData.Data.JobTitles));

                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Employee");
                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.EmployeeName);

                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Appointment Date");
                              table.Cell().Element(DataCellStyle).Background(Colors.Grey.Lighten3).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.CalendarDate);

                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Contact Report Completed on");
                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderTop(0.5f, Unit.Millimetre).Text(pdfData.Data.SurveyInstanceCompletionDate);

                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderRight(0.5f, Unit.Millimetre).BorderTop(0.5f, Unit.Millimetre).Text("Contact Report Name");
                              table.Cell().Element(DataCellStyle).Background(Colors.White).BorderTop(0.5f, Unit.Millimetre).Text(survey.Title);

                              static IContainer DataCellStyle(IContainer container)
                              {
                                  return container.DefaultTextStyle(x => x.SemiBold().FontSize(14))
                                                      .BorderBottom(1, Unit.Point)
                                                      .BorderColor(Colors.Black);
                              }
                          });

                        column.Item()
                              .PaddingTop(1, Unit.Centimetre)
                              .AlignCenter()
                              .Table(table =>
                              {
                                  //Step 1
                                  table.ColumnsDefinition(columns =>
                                  {
                                      columns.RelativeColumn();
                                  });

                                  //step 2
                                  table.Header(header =>
                                  {
                                      header.Cell()
                                            .DefaultTextStyle(x => x.SemiBold().FontSize(16).FontColor(Colors.White))
                                            .Background(Colors.Red.Darken1)
                                            .BorderBottom(2, Unit.Point)
                                            .AlignCenter()
                                            .Text("Questions");
                                  });

                                  int rowCounter = 0;
                                  //step 3
                                  foreach (var question in survey.Answers)
                                  {
                                      if (question.QuestionType == "Text" || question.QuestionType == "Number")
                                      {
                                          table.Cell().Element(DataCellStyle).Text(question.QuestionText);
                                          rowCounter++;
                                          table.Cell().Element(DataCellStyle).Text(question.AnswerText);
                                          rowCounter++;
                                      }
                                      else if (question.QuestionType == "Currency")
                                      {
                                          table.Cell().Element(DataCellStyle).Text(question.QuestionText);
                                          rowCounter++;
                                          table.Cell().Element(DataCellStyle).Text($"R: {question.AnswerText}");
                                          rowCounter++;
                                      }
                                      else if (question.QuestionType == "True/False")
                                      {
                                          table.Cell().Element(DataCellStyle).Text($"{question.QuestionText}");
                                          rowCounter++;
                                          table.Cell().Element(DataCellStyle).Text(question.AnswerText == "false" ? "No" : "True");
                                          rowCounter++;
                                      }
                                      else if (question.QuestionType == "Date")
                                      {
                                          table.Cell().Element(DataCellStyle).Text(question.QuestionText);
                                          rowCounter++;
                                          table.Cell().Element(DataCellStyle).Text(question.AnswerText);
                                          rowCounter++;
                                      }
                                      else if (question.QuestionType == "Photo")
                                      {
                                          table.Cell().Element(DataCellStyle).Text(question.QuestionText);
                                          rowCounter++;
                                          table.Cell().Element(DataCellStyle).Column(photCol =>
                                          {
                                              var photos = photoAnswers.Where(e => e.Key == question.QuestionId).Select(e => e.Value).FirstOrDefault();
                                              foreach (var item in photos)
                                              {
                                                  photCol.Item()
                                                     .AlignCenter()
                                                     .PaddingBottom(3, Unit.Point)
                                                     .Height(5, Unit.Centimetre)
                                                     .Image(item)
                                                     .FitHeight();
                                              }
                                          });
                                          rowCounter++;
                                      }
                                      else if (question.QuestionType == "Checkbox" || question.QuestionType == "Radio")
                                      {
                                          table.Cell().Element(DataCellStyle).Text(question.QuestionText);
                                          rowCounter++;
                                          foreach (var answer in question.AnswerOptions)
                                          {
                                              table.Cell().Element(DataCellStyle).Text(answer.Name);
                                          }
                                          rowCounter++;
                                      }
                                  }

                                  IContainer DataCellStyle(IContainer container)
                                  {
                                      return container.DefaultTextStyle(x => x.FontSize(12))
                                               .BorderBottom(1, Unit.Point)
                                               .BorderHorizontal(1, Unit.Point)
                                               .Background(rowCounter % 2 == 0 ? Colors.Red.Lighten5 : Colors.White)
                                               .PaddingVertical(1, Unit.Point)
                                               .AlignCenter();
                                  }
                              });

                        if (survey.Note.IsNotNullOrEmpty())
                        {
                            column.Item()
                                  .PaddingTop(1, Unit.Centimetre)
                                  .AlignCenter()
                                  .Table(table =>
                                  {
                                      table.ColumnsDefinition(column =>
                                      {
                                          column.RelativeColumn();
                                      });

                                      table.Header(header =>
                                      {
                                          header.Cell()
                                                .DefaultTextStyle(x => x.SemiBold().FontSize(16).FontColor(Colors.White))
                                                .Background(Colors.Red.Darken1)
                                                .BorderBottom(2, Unit.Point)
                                                .AlignCenter()
                                                .Text("Contact Report Note");
                                      });

                                      table.Cell()
                                           .AlignCenter()
                                           .PaddingVertical(3, Unit.Point)
                                           .DefaultTextStyle(x => x.FontSize(12))
                                           .BorderBottom(1, Unit.Point)
                                           .Text(survey.Note);
                                  });
                        }

                        if (counter != pdfData.Data.SurveySubmission.Count)
                        {
                            column.Item().PageBreak();
                        }
                    }
                });

                page.Footer()
                    .PaddingHorizontal(1f, Unit.Centimetre)
                    .Element(ComposeFooter);
            });

        });

        MemoryStream stream = new(document.GeneratePdf());

        return stream;
    }
}
