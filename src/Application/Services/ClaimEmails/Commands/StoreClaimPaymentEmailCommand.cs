using Engage.Application.Services.ClaimEmails.Models;

namespace Engage.Application.Services.ClaimEmails.Commands
{
    public class StoreClaimPaymentEmailCommand : ClaimEmailCommand, IRequest<OperationStatus>
    {
        //Required
        public decimal TotalAmount { get; set; }
        public string StoreName { get; set; }
        public List<ClaimNumber> ClaimNumbers { get; set; }
    }

    public class StoreClaimPaymentEmailCommandHandler : BaseQueryHandler, IRequestHandler<StoreClaimPaymentEmailCommand, OperationStatus>
    {
        private readonly SendGridOptions _sendGridOptions;
        private readonly IEmailService _emailService;
        public StoreClaimPaymentEmailCommandHandler(IAppDbContext context, IMapper mapper, IEmailService emailService, IOptions<SendGridOptions> sendGridOptions) : base(context, mapper) 
        {
            _sendGridOptions = sendGridOptions.Value;
            _emailService = emailService;
        }

        public async Task<OperationStatus> Handle(StoreClaimPaymentEmailCommand command, CancellationToken cancellationToken)
        {

            var templateProps = new StoreClaimPaymentTemplateProps
            {
                Name = command.Name,
                TotalAmount = command.TotalAmount,
                StoreName = command.StoreName,
                ClaimNumbers = command.ClaimNumbers,
            };

            return await _emailService.SendClaimEmailAsync(new ClaimEmail
            {
                EmailAddress = command.EmailAddress,
                Subject = command.Subject,
                CcEmailAddresses = command.CcEmailAddresses,
                StoreClaimPaymentTemplateProps = templateProps,
                ClaimEmailTypeId = EmailTypeId.ClaimPayment,
            });
        }

    }
}
