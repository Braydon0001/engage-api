using Engage.Application.Services.ClaimEmails.Models;

namespace Engage.Application.Services.ClaimEmails.Commands
{
    public class DisputedClaimEmailCommand : ClaimEmailCommand, IRequest<OperationStatus>
    {
        //Required
        public string ApproverName { get; set; }
        public string DisputedReason { get; set; }
    }

    public class DisputedClaimEmailCommandHandler : BaseQueryHandler, IRequestHandler<DisputedClaimEmailCommand, OperationStatus>
    {
        private readonly SendGridOptions _sendGridOptions;
        private readonly IEmailService _emailService;
        public DisputedClaimEmailCommandHandler(IAppDbContext context, IMapper mapper, IEmailService emailService, IOptions<SendGridOptions> sendGridOptions) : base(context, mapper) 
        {
            _sendGridOptions = sendGridOptions.Value;
            _emailService = emailService;
        }

        public async Task<OperationStatus> Handle(DisputedClaimEmailCommand command, CancellationToken cancellationToken)
        {
            var templateProps = new DisputedClaimTemplateProps
            {
                Name = command.Name,
                ApproverName = command.ApproverName,
                ClaimNumber = command.ClaimNumber,
                DisputedReason = command.DisputedReason,
            };

            return await _emailService.SendClaimEmailAsync(new ClaimEmail
            {
                EmailAddress = command.EmailAddress,
                Subject = command.Subject,
                CcEmailAddresses = command.CcEmailAddresses,
                DisputedClaimTemplateProps = templateProps,
                ClaimEmailTypeId = EmailTypeId.ClaimDisputed,
            });
        }

    }
}
