using Engage.Application.Services.EmployeeStoreCalendars.Queries;
using Engage.Application.Services.Orders.Models;
using Engage.Application.Services.OrderStagings.Queries;

namespace Engage.Application.Interfaces;

public interface IPdfService
{
    Task<MemoryStream> GenerateOrderSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : OrderSummaryPdfVm;
    Task<MemoryStream> GenerateOrderStagingSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : OrderStagingPdfDto;
    Task<MemoryStream> GenerateContactReportSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : EmployeeStoreCalendarGeneratePdfReportDto;
    Task<MemoryStream> GenerateSurveyFormContactReportSummaryPdfStream<T>(PdfModel<T> pdfData, CancellationToken cancellationToken) where T : EmployeeStoreCalendarGenerateSurveyFormPdfReportDto;
}
