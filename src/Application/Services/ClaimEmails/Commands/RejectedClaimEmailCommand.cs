using Engage.Application.Services.ClaimEmails.Models;

namespace Engage.Application.Services.ClaimEmails.Commands
{
    public class RejectedClaimEmailCommand : ClaimEmailCommand, IRequest<OperationStatus>
    {
        //Required
        public string ApproverName { get; set; }
        public string RejectedReason { get; set; }
    }

    public class RejectedClaimEmailCommandHandler : BaseQueryHandler, IRequestHandler<RejectedClaimEmailCommand, OperationStatus>
    {
        private readonly SendGridOptions _sendGridOptions;
        private readonly IEmailService _emailService;
        public RejectedClaimEmailCommandHandler(IAppDbContext context, IMapper mapper, IEmailService emailService, IOptions<SendGridOptions> sendGridOptions) : base(context, mapper) 
        {
            _sendGridOptions = sendGridOptions.Value;
            _emailService = emailService;
        }

        public async Task<OperationStatus> Handle(RejectedClaimEmailCommand command, CancellationToken cancellationToken)
        {
            var templateProps = new RejectedClaimTemplateProps
            {
                Name = command.Name,
                ApproverName = command.ApproverName,
                ClaimNumber = command.ClaimNumber,
                RejectedReason = command.RejectedReason,
            };

            return await _emailService.SendClaimEmailAsync(new ClaimEmail
            {
                EmailAddress = command.EmailAddress,
                Subject = command.Subject,
                CcEmailAddresses = command.CcEmailAddresses,
                RejectedClaimTemplateProps = templateProps,
                ClaimEmailTypeId = EmailTypeId.ClaimRejected,
            });
        }

    }
}
