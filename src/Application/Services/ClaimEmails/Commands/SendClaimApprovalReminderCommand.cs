using Engage.Application.Services.CommunicationHistories.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.ClaimEmails.Commands
{
    public class SendClaimApprovalReminderCommand : GetQuery, IRequest<OperationStatus>
    {
        public int[] AccountManagerIDs { get; set; }
        public DateTime CutOffDate { get; set; }
    }
    public class SendClaimApprovalReminderCommandHandler : IRequestHandler<SendClaimApprovalReminderCommand, OperationStatus>
    {
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;
        private readonly IUserService _user;
        private readonly IDateTimeService _dateTime;
        private readonly IEmailService _email;
        private readonly FeatureSwitchOptions _featureSwitch;
        public SendClaimApprovalReminderCommandHandler(IAppDbContext context, IMediator mediator, IUserService user, IDateTimeService dateTime, IEmailService emailService, IOptions<FeatureSwitchOptions> featureSwitch)//(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _context = context;
            _mediator = mediator;
            _user = user;
            _dateTime = dateTime;
            _email = emailService;
            _featureSwitch = featureSwitch.Value;
        }

        public async Task<OperationStatus> Handle(SendClaimApprovalReminderCommand command, CancellationToken cancellationToken)
        {
            var claimAccountManagers = await _context.Users.Where(u => command.AccountManagerIDs.Contains(u.UserId)).ToListAsync();

            if (claimAccountManagers.Count > 0)
            {
                foreach (var accountManager in claimAccountManagers)
                {

                    var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.ClaimApprovalReminder &&
                                                                                    c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                                        .FirstOrDefaultAsync(cancellationToken);
                    if (template != null)
                    {
                        var templateData = new
                        {
                            Name = accountManager.FirstName,
                            CutOffDate = command.CutOffDate.ToShortDateString(),
                        };
                        //Save History
                        await _mediator.Send(new CommunicationHistoryInsertCommand
                        {
                            CommunicationTemplateId = template.CommunicationTemplateId,
                            ToEmail = accountManager.Email,
                            FromEmail = template.FromEmailAddress,
                            FromName = template.FromName,
                            Subject = template.Subject,
                            Body = template.Body,
                            TemplateData = templateData,
                        }, cancellationToken);

                        //Send Email
                        await _mediator.Send(new SendEmailCommand
                        {
                            ToEmailAddress = accountManager.Email,
                            FromEmailAddress = template.FromEmailAddress,
                            FromEmailName = template.FromName,
                            Subject = template.Subject,
                            Body = template.Body,
                            TemplateData = templateData,
                        }, cancellationToken);

                    }

                    //if (_featureSwitch.NewApproveClaimReminderEmail)
                    //{
                    //    var templateVariables = new ClaimTemplateVariables
                    //    {
                    //        Name = accountManager.FirstName,
                    //        CutOffDate = command.CutOffDate.ToShortDateString(),
                    //    };

                    //    await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
                    //    {
                    //        EmailTypeId = EmailTypeId.ClaimApprovalReminder,
                    //        ToEmail = accountManager.Email,
                    //        Subject = "Engage Claims Approval Reminder",
                    //        TemplateVariables = templateVariables,
                    //        IsSmtp = false,
                    //        EmailBody = EmailBody.GetClaimApprovalReminderBody(accountManager.FirstName, command.CutOffDate.ToShortDateString()),
                    //    }, cancellationToken);
                    //}
                    //else
                    //{
                    //    var claimApprovalReminderCommand = new ClaimApprovalReminderCommand
                    //    {
                    //        Name = accountManager.FirstName,
                    //        EmailAddress = accountManager.Email,
                    //        CutOffDate = command.CutOffDate.ToShortDateString(),
                    //        Subject = "Engage Claims Approval Reminder",
                    //    };

                    //    await _mediator.Send(claimApprovalReminderCommand);
                    //}
                }

                return new OperationStatus
                {
                    Status = true,
                };
            }
            else
            {
                return new OperationStatus
                {
                    Status = false,
                    Message = "Account Managers Not Found",
                };
            }
        }
    }
}
