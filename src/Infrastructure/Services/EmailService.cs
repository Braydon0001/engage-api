using Engage.Application.Services.ClaimEmails.Models;
using Engage.Domain.Entities;
using Engage.Domain.Enums;
using Engage.Infrastructure.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Engage.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly SendGridOptions _sendGridOptions;
        private readonly IAppDbContext _context;

        public EmailService(IAppDbContext context, IOptions<SmtpSettings> smtpSettings, IOptions<SendGridOptions> sendGridSettings)
        {
            _smtpSettings = smtpSettings.Value;
            _sendGridOptions = sendGridSettings.Value;
            _context = context;
        }
        public async Task<OperationStatus> SendClaimPaymentNotification(string storeEmailAddress, List<string> ccEmailAddresses, string emailBody, string emailSubject)
        {
            var smtp = new SmtpClient(_smtpSettings.SmtpHost);

            smtp.Port = _smtpSettings.SmtpPort;
            smtp.Credentials = new System.Net.NetworkCredential(_smtpSettings.SmtpName, _smtpSettings.SmtpPassword);
            smtp.EnableSsl = true;

            var message = new MailMessage();

            try
            {
                message.From = new MailAddress(_smtpSettings.SmtpName);

                message.To.Add(new MailAddress(storeEmailAddress.Trim()));

                if (ccEmailAddresses.Count > 0)
                {
                    //Bcc Email addresses - Store doesn't need to see who is Cc'd on the email
                    foreach (var emailAddress in ccEmailAddresses)
                    {
                        message.Bcc.Add(new MailAddress(emailAddress.Trim()));
                    }
                }

                message.Subject = emailSubject;
                message.Body = emailBody;
                message.IsBodyHtml = true;
            }
            catch (Exception ex)
            {
                //building email failed
                return new OperationStatus
                {
                    Status = false,
                    Message = "Error Building Email.",
                    ExceptionMessage = ex.Message,
                    ExceptionInnerMessage = ex.InnerException.Message,
                    ExceptionStackTrace = ex.StackTrace,
                };
            }

            try
            {
                await smtp.SendMailAsync(message);

                //success
                return new OperationStatus
                {
                    Status = true,
                };
            }
            catch (Exception ex)
            {
                //sending email failed
                return new OperationStatus
                {
                    Status = false,
                    Message = "Error Sending Email.",
                    ExceptionMessage = ex.Message,
                    ExceptionInnerMessage = ex.InnerException.Message,
                    ExceptionStackTrace = ex.StackTrace,
                };
            }
        }

        public Task SendEmailAsync(string email, List<string> ccEmailAddresses, string emailBody, string subject)
        {
            return Execute(email, ccEmailAddresses, emailBody, subject);
        }

        private Task Execute(string email, List<string> ccEmailAddresses, string emailBody, string subject, List<System.Net.Mail.Attachment> attachments = null)
        {
            var smtp = new SmtpClient(_smtpSettings.SmtpHost);

            smtp.Port = _smtpSettings.SmtpPort;
            smtp.Credentials = new System.Net.NetworkCredential(_smtpSettings.SmtpName, _smtpSettings.SmtpPassword);
            smtp.EnableSsl = true;

            var message = new MailMessage();

            message.From = new MailAddress(_smtpSettings.SmtpName);

            message.To.Add(new MailAddress(email.Trim()));

            if (ccEmailAddresses.Count > 0)
            {
                //Bcc Email addresses - Store doesn't need to see who is Cc'd on the email
                foreach (var emailAddress in ccEmailAddresses)
                {
                    message.Bcc.Add(new MailAddress(emailAddress.Trim()));
                }
            }

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attachment in attachments)
                {
                    message.Attachments.Add(attachment);
                }
            }

            message.Subject = subject;
            message.Body = emailBody;
            message.IsBodyHtml = true;

            return smtp.SendMailAsync(message);
        }


        public async Task<Response> SendEmailAsyncSendGrid(string email, string name)
        {
            return await ExecuteSendGrid(email, name);
        }

        private async Task<Response> ExecuteSendGrid(string email, string name)
        {
            var sendGridClient = new SendGridClient(_sendGridOptions.ApiKey);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(_sendGridOptions.FromEmailAddress, _sendGridOptions.FromName);
            sendGridMessage.AddTo(email);
            sendGridMessage.SetTemplateId(_sendGridOptions.ClaimPaymentTemplateId);
            sendGridMessage.SetTemplateData(new SendGridTemplateProperty
            {
                Name = name,
            });

            var response = await sendGridClient.SendEmailAsync(sendGridMessage);
            return response;
        }

        public async Task<Response> SendEmailAsyncSendGrid(SendGridMessage sendGridMessage)
        {
            return await ExecuteSendGrid(sendGridMessage);
        }

        private async Task<Response> ExecuteSendGrid(SendGridMessage sendGridMessage)
        {
            var sendGridClient = new SendGridClient(_sendGridOptions.ApiKey);

            var response = await sendGridClient.SendEmailAsync(sendGridMessage);
            return response;
        }

        public async Task<OperationStatus> SendClaimEmailAsync(ClaimEmail claimEmail)
        {
            var sendGridClient = new SendGridClient(_sendGridOptions.ApiKey);

            //get right template
            string templateId = "";
            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimPayment)
            {
                templateId = _sendGridOptions.ClaimPaymentTemplateId;
            }

            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimRejected)
            {
                templateId = _sendGridOptions.ClaimRejectedTemplateId;
            }

            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimDisputed)
            {
                templateId = _sendGridOptions.ClaimDisputedTemplateId;
            }

            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimApprovalReminder)
            {
                templateId = _sendGridOptions.ClaimApprovalReminderTemplateId;
            }

            //build message
            var sendGridMessage = new SendGridMessage();

            sendGridMessage.SetFrom(_sendGridOptions.FromEmailAddress, _sendGridOptions.FromName);
            sendGridMessage.AddTo(claimEmail.EmailAddress);
            sendGridMessage.Subject = claimEmail.Subject;
            sendGridMessage.SetTemplateId(templateId);
            sendGridMessage.SetTemplateId(templateId);

            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimPayment)
            {
                sendGridMessage.SetTemplateData(new StoreClaimPaymentTemplateProps
                {
                    Name = claimEmail.StoreClaimPaymentTemplateProps.Name,
                    ClaimNumber = claimEmail.StoreClaimPaymentTemplateProps.ClaimNumber,
                    TotalAmount = claimEmail.StoreClaimPaymentTemplateProps.TotalAmount,
                    StoreName = claimEmail.StoreClaimPaymentTemplateProps.StoreName,
                    ClaimNumbers = claimEmail.StoreClaimPaymentTemplateProps.ClaimNumbers,
                });
            }

            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimRejected)
            {
                sendGridMessage.SetTemplateData(new RejectedClaimTemplateProps
                {
                    Name = claimEmail.RejectedClaimTemplateProps.Name,
                    ApproverName = claimEmail.RejectedClaimTemplateProps.ApproverName,
                    ClaimNumber = claimEmail.RejectedClaimTemplateProps.ClaimNumber,
                    RejectedReason = claimEmail.RejectedClaimTemplateProps.RejectedReason,
                });
            }

            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimDisputed)
            {
                sendGridMessage.SetTemplateData(new DisputedClaimTemplateProps
                {
                    Name = claimEmail.DisputedClaimTemplateProps.Name,
                    ApproverName = claimEmail.DisputedClaimTemplateProps.ApproverName,
                    ClaimNumber = claimEmail.DisputedClaimTemplateProps.ClaimNumber,
                    DisputedReason = claimEmail.DisputedClaimTemplateProps.DisputedReason,
                });
            }

            if (claimEmail.ClaimEmailTypeId == EmailTypeId.ClaimApprovalReminder)
            {
                sendGridMessage.SetTemplateData(new ClaimApprovalReminderTemplateProps
                {
                    Name = claimEmail.ClaimApprovalReminderTemplateProps.Name,
                    CutOffDate = claimEmail.ClaimApprovalReminderTemplateProps.CutOffDate,
                });
            }

            if (claimEmail.CcEmailAddresses.Count > 0)
            {
                foreach (var email in claimEmail.CcEmailAddresses)
                {
                    sendGridMessage.AddBcc(email);
                }
            }

            var response = await sendGridClient.SendEmailAsync(sendGridMessage);
            return new OperationStatus
            {
                Status = response.IsSuccessStatusCode
            };
        }

        public async Task<OperationStatus> SendEmail2Async<T>(EmailModel<T> email, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);
            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }

            if (email.IsSmtp)
            {
                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject);

                return new OperationStatus();
            }
            else
            {
                var client = new SendGridClient(_sendGridOptions.ApiKey);
                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);
                message.AddTo(email.ToEmail);
                message.Subject = email.Subject;
                message.SetTemplateId(template.ExternalTemplateId);
                message.SetTemplateData(email.TemplateVariables);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "ApproverName")
                    {
                        templateVariable.ApproverName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "ClaimNumber")
                    {
                        templateVariable.ClaimNumber = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "RejectedReason")
                    {
                        templateVariable.RejectedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "DisputedReason")
                    {
                        templateVariable.DisputedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "CutOffDate")
                    {
                        templateVariable.CutOffDate = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "TotalAmount")
                    {
                        templateVariable.TotalAmount = prop.GetValue(variables, null) == null ? 0 : Convert.ToDecimal(prop.GetValue(variables, null));
                    }

                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "ClaimNumbers")
                    {
                        var claimNumbers = prop.GetValue(variables, null);
                        if (claimNumbers != null)
                        {
                            List<Object> collection = new List<Object>((IEnumerable<Object>)claimNumbers);
                            foreach (var claimNumber in collection)
                            {
                                dynamic record = claimNumber; //Use dynamic to access properties //REF:: https://stackoverflow.com/questions/15775514/object-does-not-contain-a-definition-for-property-and-no-extension-method
                                var claimNo = new EmailTemplateVariableClaimNumber
                                {
                                    EmailHistoryTemplateVariable = templateVariable,
                                    Amount = Convert.ToDecimal(record.Amount),
                                    ClaimNo = record.ClaimNo.ToString(),
                                };
                                _context.EmailTemplateVariableClaimNumbers.Add(claimNo);
                            }
                        }
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus
                {
                    Status = response.IsSuccessStatusCode
                };
            }
        }

        //send termination email
        public async Task<OperationStatus> SendEmailTerminationAsync<T>(EmailModel<T> email, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);
            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }

            if (email.IsSmtp)
            {
                //smtp
                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject);
                return new OperationStatus();
            }
            else
            {
                var client = new SendGridClient(_sendGridOptions.ApiKey);
                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);
                message.AddTo(email.ToEmail);
                message.Subject = email.Subject;
                message.SetTemplateId(template.ExternalTemplateId);
                message.SetTemplateData(email.TemplateVariables);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "ApproverName")
                    {
                        templateVariable.ApproverName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "ClaimNumber")
                    {
                        templateVariable.ClaimNumber = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "RejectedReason")
                    {
                        templateVariable.RejectedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "DisputedReason")
                    {
                        templateVariable.DisputedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "CutOffDate")
                    {
                        templateVariable.CutOffDate = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "TotalAmount")
                    {
                        templateVariable.TotalAmount = prop.GetValue(variables, null) == null ? 0 : Convert.ToDecimal(prop.GetValue(variables, null));
                    }

                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "ClaimNumbers")
                    {
                        var claimNumbers = prop.GetValue(variables, null);
                        if (claimNumbers != null)
                        {
                            List<Object> collection = new List<Object>((IEnumerable<Object>)claimNumbers);
                            foreach (var claimNumber in collection)
                            {
                                dynamic record = claimNumber; //Use dynamic to access properties //REF:: https://stackoverflow.com/questions/15775514/object-does-not-contain-a-definition-for-property-and-no-extension-method
                                var claimNo = new EmailTemplateVariableClaimNumber
                                {
                                    EmailHistoryTemplateVariable = templateVariable,
                                    Amount = Convert.ToDecimal(record.Amount),
                                    ClaimNo = record.ClaimNo.ToString(),
                                };
                                _context.EmailTemplateVariableClaimNumbers.Add(claimNo);
                            }
                        }
                    }
                    if (prop.Name == "EmployeeName")
                    {
                        templateVariable.EmployeeName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "EmployeeCode")
                    {
                        templateVariable.EmployeeName = prop.GetValue(variables, null) == null ? templateVariable.EmployeeName : templateVariable.EmployeeName + prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminationDate")
                    {
                        templateVariable.TerminationDate = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminationReason")
                    {
                        templateVariable.TerminationReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminatedBy")
                    {
                        templateVariable.TerminatorName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus
                {
                    Status = true
                };
            }
        }

        public async Task<OperationStatus> SendEmailCalendarCurrentPeriodReportAsync<T>(EmailModel<T> email, MemoryStream attachment, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);
            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }
            if (email.IsSmtp)
            {
                List<System.Net.Mail.Attachment> attachmentFiles = new List<System.Net.Mail.Attachment>
                {
                    new System.Net.Mail.Attachment(attachment, $"{DateTime.Now.ToShortDateString()} - Report.xlsx", "application/octet-stream")
                };

                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject, attachmentFiles);

                return new OperationStatus();
            }
            else
            {
                SendGrid.Helpers.Mail.Attachment report = new SendGrid.Helpers.Mail.Attachment
                {
                    Content = Convert.ToBase64String(attachment.ToArray()),
                    Type = "application/octet-stream",
                    Filename = $"{DateTime.Now.ToShortDateString()} - Report.xlsx",
                };

                var client = new SendGridClient(_sendGridOptions.ApiKey);

                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);

                message.AddTo(email.ToEmail);

                message.Subject = email.Subject;

                message.SetTemplateId(template.ExternalTemplateId);

                message.SetTemplateData(email.TemplateVariables);

                message.AddAttachment(report);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "ApproverName")
                    {
                        templateVariable.ApproverName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "ClaimNumber")
                    {
                        templateVariable.ClaimNumber = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "RejectedReason")
                    {
                        templateVariable.RejectedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "DisputedReason")
                    {
                        templateVariable.DisputedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "CutOffDate")
                    {
                        templateVariable.CutOffDate = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "TotalAmount")
                    {
                        templateVariable.TotalAmount = prop.GetValue(variables, null) == null ? 0 : Convert.ToDecimal(prop.GetValue(variables, null));
                    }

                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "ClaimNumbers")
                    {
                        var claimNumbers = prop.GetValue(variables, null);
                        if (claimNumbers != null)
                        {
                            List<Object> collection = new List<Object>((IEnumerable<Object>)claimNumbers);
                            foreach (var claimNumber in collection)
                            {
                                dynamic record = claimNumber; //Use dynamic to access properties //REF:: https://stackoverflow.com/questions/15775514/object-does-not-contain-a-definition-for-property-and-no-extension-method
                                var claimNo = new EmailTemplateVariableClaimNumber
                                {
                                    EmailHistoryTemplateVariable = templateVariable,
                                    Amount = Convert.ToDecimal(record.Amount),
                                    ClaimNo = record.ClaimNo.ToString(),
                                };
                                _context.EmailTemplateVariableClaimNumbers.Add(claimNo);
                            }
                        }
                    }
                    if (prop.Name == "EmployeeName")
                    {
                        templateVariable.EmployeeName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "EmployeeCode")
                    {
                        templateVariable.EmployeeName = prop.GetValue(variables, null) == null ? templateVariable.EmployeeName : templateVariable.EmployeeName + prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminationDate")
                    {
                        templateVariable.TerminationDate = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminationReason")
                    {
                        templateVariable.TerminationReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminatedBy")
                    {
                        templateVariable.TerminatorName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "EmployeeId")
                    {
                        templateVariable.EmployeeId = prop.GetValue(variables, null) == null ? 0 : Convert.ToInt32(prop.GetValue(variables, null));
                    }
                    if (prop.Name == "ReportDate")
                    {
                        templateVariable.ReportDate = prop.GetValue(variables, null) == null ? DateTime.Now : DateTime.Parse(prop.GetValue(variables, null).ToString());
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus(response.IsSuccessStatusCode);
            }
        }

        public async Task<OperationStatus> SendEmailCalendarPreviousPeriodReportAsync<T>(EmailModel<T> email, MemoryStream attachment, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);
            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }
            if (email.IsSmtp)
            {
                List<System.Net.Mail.Attachment> attachmentFiles = new List<System.Net.Mail.Attachment>
                {
                    new System.Net.Mail.Attachment(attachment, $"{DateTime.Now.ToShortDateString()} Last Week's Report.xlsx", "application/octet-stream")
                };
                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject, attachmentFiles);

                return new OperationStatus();
            }
            else
            {

                SendGrid.Helpers.Mail.Attachment report = new SendGrid.Helpers.Mail.Attachment
                {
                    Content = Convert.ToBase64String(attachment.ToArray()),
                    Type = "application/octet-stream",
                    Filename = $"{DateTime.Now.ToShortDateTimeString()} Last Week's Report.xlsx",
                };

                var client = new SendGridClient(_sendGridOptions.ApiKey);

                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);

                message.AddTo(email.ToEmail);

                message.Subject = email.Subject;

                message.SetTemplateId(template.ExternalTemplateId);

                message.SetTemplateData(email.TemplateVariables);

                message.AddAttachment(report);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "ApproverName")
                    {
                        templateVariable.ApproverName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "ClaimNumber")
                    {
                        templateVariable.ClaimNumber = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "RejectedReason")
                    {
                        templateVariable.RejectedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "DisputedReason")
                    {
                        templateVariable.DisputedReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "CutOffDate")
                    {
                        templateVariable.CutOffDate = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }

                    if (prop.Name == "TotalAmount")
                    {
                        templateVariable.TotalAmount = prop.GetValue(variables, null) == null ? 0 : Convert.ToDecimal(prop.GetValue(variables, null));
                    }

                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "ClaimNumbers")
                    {
                        var claimNumbers = prop.GetValue(variables, null);
                        if (claimNumbers != null)
                        {
                            List<Object> collection = new List<Object>((IEnumerable<Object>)claimNumbers);
                            foreach (var claimNumber in collection)
                            {
                                dynamic record = claimNumber; //Use dynamic to access properties //REF:: https://stackoverflow.com/questions/15775514/object-does-not-contain-a-definition-for-property-and-no-extension-method
                                var claimNo = new EmailTemplateVariableClaimNumber
                                {
                                    EmailHistoryTemplateVariable = templateVariable,
                                    Amount = Convert.ToDecimal(record.Amount),
                                    ClaimNo = record.ClaimNo.ToString(),
                                };
                                _context.EmailTemplateVariableClaimNumbers.Add(claimNo);
                            }
                        }
                    }
                    if (prop.Name == "EmployeeName")
                    {
                        templateVariable.EmployeeName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "EmployeeCode")
                    {
                        templateVariable.EmployeeName = prop.GetValue(variables, null) == null ? templateVariable.EmployeeName : templateVariable.EmployeeName + prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminationDate")
                    {
                        templateVariable.TerminationDate = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminationReason")
                    {
                        templateVariable.TerminationReason = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "TerminatedBy")
                    {
                        templateVariable.TerminatorName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "EmployeeId")
                    {
                        templateVariable.EmployeeId = prop.GetValue(variables, null) == null ? 0 : Convert.ToInt32(prop.GetValue(variables, null));
                    }
                    if (prop.Name == "ReportDate")
                    {
                        templateVariable.ReportDate = prop.GetValue(variables, null) == null ? DateTime.Now : DateTime.Parse(prop.GetValue(variables, null).ToString());
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus(response.IsSuccessStatusCode);
            }
        }

        public async Task<OperationStatus> SendEmailContactReportCompleteAsync<T>(EmailModel<T> email, MemoryStream attachment, string fileName, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);

            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }
            if (email.IsSmtp)
            {
                List<System.Net.Mail.Attachment> attachmentFiles = new List<System.Net.Mail.Attachment>
                {
                    new System.Net.Mail.Attachment(attachment, fileName)
                };

                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject, attachmentFiles);

                return new OperationStatus();
            }
            else
            {
                SendGrid.Helpers.Mail.Attachment report = new SendGrid.Helpers.Mail.Attachment
                {
                    Content = Convert.ToBase64String(attachment.ToArray()),
                    Type = "application/pdf",
                    Filename = fileName,
                };

                var client = new SendGridClient(_sendGridOptions.ApiKey);

                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);

                message.AddTo(email.ToEmail);

                message.Subject = email.Subject;

                message.SetTemplateId(template.ExternalTemplateId);

                message.SetTemplateData(email.TemplateVariables);

                message.AddAttachment(report);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "SurveyInstanceId")
                    {
                        templateVariable.SurveyInstanceId = prop.GetValue(variables, null) == null ? 0 : Convert.ToInt32(prop.GetValue(variables, null));
                    }
                    if (prop.Name == "ReportDate")
                    {
                        templateVariable.ReportDate = prop.GetValue(variables, null) == null ? DateTime.Now : DateTime.Parse(prop.GetValue(variables, null).ToString());
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus
                {
                    Status = response.IsSuccessStatusCode
                };
            }
        }

        public async Task<OperationStatus> SendEmailOrderSubmitAsync<T>(EmailModel<T> email, MemoryStream attachment, string orderDate, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);

            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }

            string fileName = $"Order Summary {orderDate}.pdf";

            if (email.IsSmtp)
            {
                List<System.Net.Mail.Attachment> attachmentFiles = new List<System.Net.Mail.Attachment>
                {
                    new System.Net.Mail.Attachment(attachment, fileName)
                };

                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject, attachmentFiles);

                return new OperationStatus();
            }
            else
            {
                SendGrid.Helpers.Mail.Attachment report = new SendGrid.Helpers.Mail.Attachment
                {
                    Content = Convert.ToBase64String(attachment.ToArray()),
                    Type = "application/octet-stream",
                    Filename = fileName,
                };

                var client = new SendGridClient(_sendGridOptions.ApiKey);

                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);

                message.AddTo(email.ToEmail);

                message.Subject = email.Subject;

                message.SetTemplateId(template.ExternalTemplateId);

                message.SetTemplateData(email.TemplateVariables);

                message.AddAttachment(report);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "OrderId")
                    {
                        templateVariable.OrderId = prop.GetValue(variables, null) == null ? 0 : Convert.ToInt32(prop.GetValue(variables, null));
                    }
                    if (prop.Name == "OrderDate")
                    {
                        try
                        {
                            templateVariable.OrderDate = prop.GetValue(variables, null) == null ? DateTime.Now : DateTime.Parse(prop.GetValue(variables, null).ToString());
                        }
                        catch (Exception)
                        {
                            templateVariable.OrderDate = DateTime.Now;
                        }
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus
                {
                    Status = response.IsSuccessStatusCode
                };
            }
        }

        public async Task<OperationStatus> SendEmailOrderStagingSubmitAsync<T>(EmailModel<T> email, MemoryStream attachment, string orderDate, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);

            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }

            string fileName = $"Order Summary {orderDate}.pdf";

            if (email.IsSmtp)
            {
                List<System.Net.Mail.Attachment> attachmentFiles = new List<System.Net.Mail.Attachment>
                {
                    new System.Net.Mail.Attachment(attachment, fileName)
                };

                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject, attachmentFiles);

                return new OperationStatus();
            }
            else
            {
                SendGrid.Helpers.Mail.Attachment report = new SendGrid.Helpers.Mail.Attachment
                {
                    Content = Convert.ToBase64String(attachment.ToArray()),
                    Type = "application/octet-stream",
                    Filename = fileName,
                };

                var client = new SendGridClient(_sendGridOptions.ApiKey);

                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);

                message.AddTo(email.ToEmail);

                message.Subject = email.Subject;

                message.SetTemplateId(template.ExternalTemplateId);

                message.SetTemplateData(email.TemplateVariables);

                message.AddAttachment(report);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    //if (prop.Name == "OrderStagingId")
                    //{
                    //    templateVariable.OrderStagingId = prop.GetValue(variables, null) == null ? 0 : Convert.ToInt32(prop.GetValue(variables, null));
                    //}
                    if (prop.Name == "OrderDate")
                    {
                        templateVariable.OrderDate = prop.GetValue(variables, null) == null ? DateTime.Now : DateTime.Parse(prop.GetValue(variables, null).ToString());
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus
                {
                    Status = response.IsSuccessStatusCode
                };
            }
        }

        public async Task<OperationStatus> SendEmailCalendarManagerCreateVisitAsync<T>(EmailModel<T> email, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);

            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }

            if (email.IsSmtp)
            {
                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject);

                return new OperationStatus();
            }

            var client = new SendGridClient(_sendGridOptions.ApiKey);


            var message = new SendGridMessage();

            message.SetFrom(template.FromEmailAddress, template.FromEmailName);

            message.AddTo(email.ToEmail);

            message.Subject = email.Subject;

            message.SetTemplateId(template.ExternalTemplateId);

            message.SetTemplateData(email.TemplateVariables);

            if (email.CCEmails != null)
            {
                foreach (var cc in email.CCEmails)
                {
                    message.AddBcc(cc);
                }
            }

            var response = await client.SendEmailAsync(message, cancellationToken);

            var emailHistory = new EmailHistory
            {
                ToEmail = email.ToEmail,
                Subject = email.Subject,
                EmailTemplateId = template.EmailTemplateId,
                Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
            };

            _context.EmailHistories.Add(emailHistory);

            var variables = email.TemplateVariables;
            var templateVariable = new EmailHistoryTemplateVariable();
            templateVariable.EmailHistory = emailHistory;

            foreach (PropertyInfo prop in variables.GetType().GetProperties())
            {
                if (prop.Name == "Name")
                {
                    templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                }
                if (prop.Name == "StoreName")
                {
                    templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                }
                if (prop.Name == "CalendarDate")
                {
                    templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                }
            }

            _context.EmailHistoryTemplateVariables.Add(templateVariable);

            await _context.SaveChangesAsync(cancellationToken);

            return new OperationStatus
            {
                Status = response.IsSuccessStatusCode
            };
        }

        public async Task<OperationStatus> SendEmailPosUpdateAsync<T>(EmailModel<T> email, string fileName, CancellationToken cancellationToken) where T : class
        {
            var template = await _context.EmailTemplates.SingleOrDefaultAsync(e => e.EmailTypeId == (int)email.EmailTypeId, cancellationToken);

            if (template == null)
            {
                throw new NotFoundException(nameof(EmailTemplate), email.EmailTypeId);
            }
            if (email.IsSmtp)
            {
                //List<System.Net.Mail.Attachment> attachmentFiles = new List<System.Net.Mail.Attachment>
                //{
                //    new System.Net.Mail.Attachment(attachment, fileName)
                //};

                await Execute(email.ToEmail, email.CCEmails, email.EmailBody.ToString(), email.Subject);

                return new OperationStatus();
            }
            else
            {
                //SendGrid.Helpers.Mail.Attachment image = new SendGrid.Helpers.Mail.Attachment
                //{
                //    Content = Convert.ToBase64String(attachment.ToArray()),
                //    Type = "image/jpeg",
                //    Filename = fileName,
                //};

                var client = new SendGridClient(_sendGridOptions.ApiKey);

                var message = new SendGridMessage();

                message.SetFrom(template.FromEmailAddress, template.FromEmailName);

                message.AddTo(email.ToEmail);

                message.Subject = email.Subject;

                message.SetTemplateId(template.ExternalTemplateId);

                message.SetTemplateData(email.TemplateVariables);

                //message.AddAttachment(image);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        message.AddBcc(cc);
                    }
                }

                var response = await client.SendEmailAsync(message, cancellationToken);

                var emailHistory = new EmailHistory
                {
                    ToEmail = email.ToEmail,
                    Subject = email.Subject,
                    EmailTemplateId = template.EmailTemplateId,
                    Error = response.IsSuccessStatusCode ? "" : "Didn't go through to Email Service Provider",
                };

                _context.EmailHistories.Add(emailHistory);

                if (email.CCEmails != null)
                {
                    foreach (var cc in email.CCEmails)
                    {
                        var emailHistoryCcEmail = new EmailHistoryCCEmail
                        {
                            EmailHistory = emailHistory,
                            Email = cc,
                        };

                        _context.EmailHistoryCCEmails.Add(emailHistoryCcEmail);
                    }
                }

                var variables = email.TemplateVariables;
                var templateVariable = new EmailHistoryTemplateVariable();
                templateVariable.EmailHistory = emailHistory;

                foreach (PropertyInfo prop in variables.GetType().GetProperties())
                {
                    if (prop.Name == "EmployeeName")
                    {
                        templateVariable.Name = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "StoreName")
                    {
                        templateVariable.StoreName = prop.GetValue(variables, null) == null ? "" : prop.GetValue(variables, null).ToString();
                    }
                    if (prop.Name == "SubmissionId")
                    {
                        templateVariable.SurveyInstanceId = prop.GetValue(variables, null) == null ? 0 : Convert.ToInt32(prop.GetValue(variables, null));
                    }
                    if (prop.Name == "RequestDate")
                    {
                        templateVariable.ReportDate = prop.GetValue(variables, null) == null ? DateTime.Now : DateTime.Parse(prop.GetValue(variables, null).ToString());
                    }
                }

                _context.EmailHistoryTemplateVariables.Add(templateVariable);

                await _context.SaveChangesAsync(cancellationToken);

                return new OperationStatus
                {
                    Status = response.IsSuccessStatusCode
                };
            }
        }

        private static dynamic ToStringTemplate(IDictionary<string, string> dictionary)
        {
            var builder = new StringBuilder();
            foreach (var keyValuePair in dictionary)
            {
                builder.Append(keyValuePair.Key);
                builder.Append('=');
                builder.Append(keyValuePair.Value);
                builder.Append(';');
            }
            return builder.ToString();
        }

        private static string ToStringEmails(IList<string> list)
        {
            return string.Join(';', list);
        }
    }


}
