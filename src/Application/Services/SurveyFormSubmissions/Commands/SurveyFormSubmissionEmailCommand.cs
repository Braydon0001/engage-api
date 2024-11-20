using Engage.Application.Services.CommunicationHistoryStores.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormSubmissionEmailCommand : IMapTo<SurveyFormSubmission>, IRequest<OperationStatus>
{
    public int EmployeeId { get; init; }
    public int SurveyFormSubmissionId { get; init; }
    public int StoreId { get; init; }
    public string AnswerDate { get; init; }
    //public string AttachmentUrl { get; init; }
}

public record SurveyFormSubmissionEmailHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IEmailService Email, IOptions<PosUpdateEmailOptions> Options) : IRequestHandler<SurveyFormSubmissionEmailCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormSubmissionEmailCommand command, CancellationToken cancellationToken)
    {
        var employeeName = await Context.Employees.Where(e => e.EmployeeId == command.EmployeeId)
                                                  .Select(e => e.FirstName + " " + e.LastName)
                                                  .SingleOrDefaultAsync(cancellationToken);

        var store = await Context.Stores.Include(e => e.DCAccounts).Where(e => e.StoreId == command.StoreId).SingleOrDefaultAsync(cancellationToken);

        var storeDcAccount = store.DCAccounts.Where(e => e.IsPrimary).FirstOrDefault();
        if (storeDcAccount == null)
        {
            storeDcAccount = store.DCAccounts.FirstOrDefault();
        }

        var dcAccountText = storeDcAccount == null ? "" : $" ({storeDcAccount.AccountNumber})";

        //var emailVariables = new SurveyFormNotificationEmailTemplate
        //{
        //    EmployeeName = employeeName,
        //    StoreName = store.Name,
        //    RequestDate = command.AnswerDate,
        //    SubmissionId = command.SurveyFormSubmissionId,
        //    DcAccountNumber = dcAccountText
        //};

        //return await Email.SendEmailPosUpdateAsync(
        //    new EmailModel<SurveyFormNotificationEmailTemplate>
        //    {
        //        EmailTypeId = EmailTypeId.SurveyFormPOSUpdate,
        //        ToEmail = Options.Value.ToEmail,
        //        Subject = $"POS Update Request - {store.Name}{dcAccountText}",
        //        TemplateVariables = emailVariables,
        //        //CCEmails = [Options.Value.CcEmail],
        //        IsSmtp = false,
        //        EmailBody = EmailBody.GetSurveyFormPOSUpdateBody(employeeName, store.Name, command.AnswerDate, command.SurveyFormSubmissionId.ToString())
        //    },
        //    //attachment,
        //    $"POS Update Request for {store.Name} on {command.AnswerDate.Replace('/', '-')}.jpeg",
        //    cancellationToken
        //    );
        var template = await Context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.SurveyFormPOSUpdate &&
                                                                        c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                            .FirstOrDefaultAsync(cancellationToken);
        if (template != null)
        {
            var templateData = new
            {
                EmployeeName = employeeName,
                StoreName = store.Name,
                RequestDate = command.AnswerDate,
                SubmissionId = command.SurveyFormSubmissionId,
                DcAccountNumber = dcAccountText
            };
            //Save History
            await Mediator.Send(new CommunicationHistoryStoreInsertCommand
            {
                StoreId = command.StoreId,
                CommunicationTemplateId = template.CommunicationTemplateId,
                ToEmail = Options.Value.ToEmail,
                FromEmail = template.FromEmailAddress,
                FromName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
            }, cancellationToken);

            //Send Email
            await Mediator.Send(new SendEmailCommand
            {
                ToEmailAddress = Options.Value.ToEmail,
                FromEmailAddress = template.FromEmailAddress,
                FromEmailName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
            }, cancellationToken);
        }

        return new OperationStatus { Status = true };
    }
}

public class SurveyFormSubmissionEmailValidator : AbstractValidator<SurveyFormSubmissionEmailCommand>
{
    public SurveyFormSubmissionEmailValidator()
    {
        RuleFor(e => e.EmployeeId);
        RuleFor(e => e.SurveyFormSubmissionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId);
    }
}