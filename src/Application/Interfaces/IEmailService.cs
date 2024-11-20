using Engage.Application.Services.ClaimEmails.Models;

namespace Engage.Application.Interfaces;

public interface IEmailService
{
    Task<OperationStatus> SendClaimPaymentNotification(string storeEmailAddress, List<string> ccEmailAddresses, string emailBody, string emailSubject);
    Task SendEmailAsync(string email, List<string> ccEmailAddresses, string emailBody, string subject);
    Task<OperationStatus> SendEmail2Async<T>(EmailModel<T> email, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendEmailTerminationAsync<T>(EmailModel<T> email, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendClaimEmailAsync(ClaimEmail claimEmail);
    Task<OperationStatus> SendEmailCalendarCurrentPeriodReportAsync<T>(EmailModel<T> email, MemoryStream attachment, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendEmailCalendarPreviousPeriodReportAsync<T>(EmailModel<T> email, MemoryStream attachment, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendEmailContactReportCompleteAsync<T>(EmailModel<T> email, MemoryStream attachment, string fileName, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendEmailPosUpdateAsync<T>(EmailModel<T> email, string fileName, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendEmailOrderSubmitAsync<T>(EmailModel<T> email, MemoryStream attachment, string orderDate, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendEmailOrderStagingSubmitAsync<T>(EmailModel<T> email, MemoryStream attachment, string orderDate, CancellationToken cancellationToken) where T : class;
    Task<OperationStatus> SendEmailCalendarManagerCreateVisitAsync<T>(EmailModel<T> email, CancellationToken cancellationToken) where T : class;
}
