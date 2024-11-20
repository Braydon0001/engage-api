using Engage.Application.Contracts;
using Engage.Application.Services.ClaimEmails.Models;
using Engage.Application.Services.Claims.Commands;
using Engage.Application.Services.CommunicationHistoryStores.Commands;
using Engage.Application.Services.Emails.Commands;
using MassTransit;

namespace Engage.Application.Services.ClaimReports.Commands;

public class PayClaimsAndSendNotificationsCommand : GetQuery, IRequest<OperationStatus>
{
    public int[] ClaimIDs { get; set; }
}

public class PayClaimsAndSendNotificationsCommandHandler : IRequestHandler<PayClaimsAndSendNotificationsCommand, OperationStatus>, IConsumer<ClaimPaymentEmail>
{
    private readonly ClaimSettings _claimSettings;
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;
    private readonly IEmailService _email;
    private readonly FeatureSwitchOptions _featureSwitch;
    private readonly IBus _bus;
    private readonly IHandlebarsService _handlebarsService;
    public PayClaimsAndSendNotificationsCommandHandler(IAppDbContext context, IMediator mediator, IUserService user, IDateTimeService dateTime, IEmailService emailService, IOptions<FeatureSwitchOptions> featureSwitch, IOptions<ClaimSettings> claimSettings, IBus bus, IHandlebarsService handlebarsService)//(IAppDbContext context, IMapper mapper, IUserService user, IOptions<ClaimSettings> claimSettings, IEmailService emailService, IMediator mediator) : base(context, mapper)
    {
        _claimSettings = claimSettings.Value;
        _context = context;
        _mediator = mediator;
        _user = user;
        _dateTime = dateTime;
        _email = emailService;
        _featureSwitch = featureSwitch.Value;
        _bus = bus;
        _handlebarsService = handlebarsService;
    }

    public async Task<OperationStatus> Handle(PayClaimsAndSendNotificationsCommand command, CancellationToken cancellationToken)
    {
        var claimsToPay = await _context.Claims
                                   .Include(c => c.Store)
                                   .Include(c => c.Store.StoreContacts)
                                   .Include(c => c.ClaimSkus)
                                   .Where(e => command.ClaimIDs.Contains(e.ClaimId))
                                   .ToListAsync();

        if (claimsToPay.Count > 0)
        {
            var batchUpdateStatusCommand = new BatchUpdateClaimStatusCommand
            {
                ClaimIds = claimsToPay.Select(c => c.ClaimId).ToList(),
                ClaimStatusId = (int)ClaimStatusId.Paid,
                ClaimClassificationId = claimsToPay.First().ClaimClassificationId,
                EngageRegionId = claimsToPay.First().Store.EngageRegionId,
            };

            var updated = await _mediator.Send(batchUpdateStatusCommand);

            var stores = claimsToPay
                              .GroupBy(s => new { s.StoreId, s.Store.Name })
                              .Select(c => new { StoreId = c.Key.StoreId, StoreName = c.Key.Name })
                              .ToList();

            foreach (var store in stores)
            {
                var storeClaims = claimsToPay.Where(s => s.StoreId == store.StoreId).ToList();

                string contactPerson = "Sir/Mam";
                string emailAddress = _claimSettings.DefaultStoreClaimPaymentEmailAddress;

                if (storeClaims.First().Store.StoreContacts.Count > 0)
                {
                    var claimContact = storeClaims.First().Store.StoreContacts
                                        .Where(c => c.EntityContactTypeId == (int)EntityContactTypeId.ClaimPaymentNotifier)
                                        .FirstOrDefault();

                    if (claimContact != null)
                    {
                        emailAddress = string.IsNullOrWhiteSpace(claimContact.EmailAddress1.ToString()) ? emailAddress : claimContact.EmailAddress1.ToString();
                        contactPerson = claimContact.FirstName.ToString();
                    }
                }


                var ccEmailAddresses = await _context.ClaimNotificationUsers
                            .Include(c => c.User)
                            .Where(c => c.User.Disabled == false && c.User.Deleted == false && c.ClaimStatusId == (int)ClaimStatusId.Paid && c.EngageRegionId == storeClaims.First().Store.EngageRegionId)
                            .Select(c => c.User.Email)
                            .ToListAsync();

                ccEmailAddresses?.RemoveIfContains(emailAddress);

                decimal storeTotal = 0;
                var claimNumbers = new List<ClaimNumber>();
                foreach (var claim in storeClaims)
                {
                    var total = claim.ClaimSkus.Where(c => c.Deleted == false).Sum(c => c.TotalAmount);
                    storeTotal = storeTotal + total;

                    var claimNumber = new ClaimNumber();
                    claimNumber.ClaimNo = claim.ClaimNumber;
                    claimNumber.Amount = Math.Round(total, 2);

                    claimNumbers.Add(claimNumber);
                }

                var emailSubject = "Engage Claim Payment Notification: " + store.StoreName + " - Claim Total R" + Math.Round(storeTotal, 2);

                var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.StoreClaimPayment &&
                                                                                c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                                    .FirstOrDefaultAsync(cancellationToken);

                if (template != null)
                {
                    try
                    {
                        //Save History
                        await _mediator.Send(new CommunicationHistoryStoreInsertCommand
                        {
                            StoreId = store.StoreId,
                            CommunicationTemplateId = template.CommunicationTemplateId,
                            ToEmail = emailAddress,
                            FromEmail = template.FromEmailAddress,
                            FromName = template.FromName,
                            Subject = template.Subject,
                            Body = template.Body,
                            CcEmails = ccEmailAddresses.Count > 0 ? string.Join(", ", ccEmailAddresses) : null,
                            TemplateData = new
                            {
                                StoreName = store.StoreName,
                                TotalAmount = Math.Round(storeTotal, 2),
                                Name = contactPerson,
                                ClaimNumbers = claimNumbers,
                            }
                        }, cancellationToken);

                        //Send Email
                        //await _mediator.Send(new SendEmailCommand
                        //{
                        //    ToEmailAddress = emailAddress,
                        //    FromEmailAddress = template.FromEmailAddress,
                        //    FromEmailName = template.FromName,
                        //    CcEmailAddresses = ccEmailAddresses,
                        //    Subject = template.Subject,
                        //    Body = template.Body,
                        //    TemplateData = new
                        //    {
                        //        StoreName = store.StoreName,
                        //        TotalAmount = Math.Round(storeTotal, 2),
                        //        Name = contactPerson,
                        //        ClaimNumbers = claimNumbers,
                        //    }
                        //}, cancellationToken);
                        var templateDate = new
                        {
                            StoreName = store.StoreName,
                            TotalAmount = Math.Round(storeTotal, 2),
                            Name = contactPerson,
                            ClaimNumbers = claimNumbers,
                        };

                        var subject = _handlebarsService.RenderTemplate(template.Subject, templateDate);
                        var body = _handlebarsService.RenderTemplate(template.Body, templateDate);

                        await _bus.Publish(new ClaimPaymentEmail
                        {
                            ToEmailAddress = emailAddress,
                            FromEmailAddress = template.FromEmailAddress,
                            FromEmailName = template.FromName,
                            CcEmailAddresses = ccEmailAddresses,
                            Subject = subject,
                            Body = body,
                        }, cancellationToken);
                    }
                    catch (Exception) { }
                }

                //await _bus.Publish(new ClaimPaymentEmail
                //{
                //    ContactPerson = contactPerson,
                //    StoreTotal = storeTotal,
                //    StoreName = store.StoreName,
                //    ClaimNumbers = claimNumbers,
                //    EmailSubject = emailSubject,
                //    EmailAddress = emailAddress,
                //    CcEmailAddresses = ccEmailAddresses,
                //}, cancellationToken);
            }

            try
            {

                return updated;
            }
            catch (Exception)
            {
                return new OperationStatus
                {
                    Status = false,
                    Message = "Error updating claim status",
                };
            }
        }
        else
        {
            return new OperationStatus
            {
                Status = false,
                Message = "Error: Could not find claims to pay",
            };
        }
    }

    public async Task Consume(ConsumeContext<ClaimPaymentEmail> context)
    {
        //Send Email
        await _mediator.Send(new SendEmailByBusCommand
        {
            ToEmailAddress = context.Message.ToEmailAddress,
            FromEmailAddress = context.Message.FromEmailAddress,
            FromEmailName = context.Message.FromEmailName,
            CcEmailAddresses = context.Message.CcEmailAddresses,
            Subject = context.Message.Subject,
            Body = context.Message.Body,
        });
    }
}
