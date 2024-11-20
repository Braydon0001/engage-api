using Engage.Application.Services.ClaimEmails.Models;

namespace Engage.Application.Services.ClaimEmails.Commands
{
    public class ClaimApprovalReminderCommand : ClaimEmailCommand, IRequest<OperationStatus>
    {
        public string CutOffDate { get;set;}
    }

    public class ClaimApprovalReminderCommandHandler : BaseQueryHandler, IRequestHandler<ClaimApprovalReminderCommand, OperationStatus>
    {
        private readonly SendGridOptions _sendGridOptions;
        private readonly IEmailService _emailService;
        public ClaimApprovalReminderCommandHandler(IAppDbContext context, IMapper mapper, IEmailService emailService, IOptions<SendGridOptions> sendGridOptions) : base(context, mapper) 
        {
            _sendGridOptions = sendGridOptions.Value;
            _emailService = emailService;
        }

        public async Task<OperationStatus> Handle(ClaimApprovalReminderCommand command, CancellationToken cancellationToken)
        {

            var templateProps = new ClaimApprovalReminderTemplateProps
            {
                Name = command.Name,
                CutOffDate = command.CutOffDate,
            };

            return await _emailService.SendClaimEmailAsync(new ClaimEmail
            {
                EmailAddress = command.EmailAddress,
                Subject = command.Subject,
                CcEmailAddresses = new List<string>(),
                ClaimEmailTypeId = EmailTypeId.ClaimApprovalReminder,
                ClaimApprovalReminderTemplateProps = templateProps,
            });
        }

    }
}
