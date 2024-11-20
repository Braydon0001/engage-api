using Engage.Application.Services.CommunicationHistoryClaimFloats.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.ClaimFloats.Commands;

public class UpdateClaimFloatClaimCommand : IRequest<OperationStatus>
{
    public Claim Claim { get; set; }
    public int EngageRegionId { get; set; }
    public int ClaimStatusId { get; set; }
    public bool SaveChanges { get; set; } = true;

    public UpdateClaimFloatClaimCommand(Claim claim, int engageRegionId, int claimStatusId = 0, bool saveChanges = true)
    {
        Claim = claim;
        EngageRegionId = engageRegionId;
        ClaimStatusId = claimStatusId;
        SaveChanges = saveChanges;
    }
}

public class UpdateClaimFloatClaimCommandHandler : BaseCreateCommandHandler, IRequestHandler<UpdateClaimFloatClaimCommand, OperationStatus>
{
    private readonly ClaimSettings _claimSettings;
    private readonly IEmailService _email;
    public UpdateClaimFloatClaimCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IOptions<ClaimSettings> claimSettings, IEmailService email) : base(context, mapper, mediator)
    {
        _claimSettings = claimSettings.Value;
        _email = email;
    }

    public async Task<OperationStatus> Handle(UpdateClaimFloatClaimCommand command, CancellationToken cancellationToken)
    {
        var claimFloats = await _context.ClaimFloats.Include(c => c.ClaimFloatClaims)
                                                    .Include(c => c.Supplier)
                                                    .Where(c => c.SupplierId == command.Claim.SupplierId && c.EngageRegionId == command.EngageRegionId)
                                                    .ToListAsync(cancellationToken);

        if (claimFloats.Count > 0)
        {
            if (command.Claim.ClaimFloatId.HasValue)
            {
                var claimFloat = claimFloats.Where(c => c.ClaimFloatId == command.Claim.ClaimFloatId.Value)
                                            .FirstOrDefault();

                if (claimFloat != null)
                {
                    if (command.ClaimStatusId == (int)ClaimStatusId.Approved)
                    {
                        decimal amountUsed = 0;
                        decimal amountAvailable = 0;

                        if (claimFloat.ClaimFloatClaims.Count > 0)
                        {
                            foreach (var record in claimFloat.ClaimFloatClaims)
                            {
                                //Calculate used, available, Can we save this new record?
                                var claim = await _context.Claims.Include(c => c.ClaimSkus)
                                                                 .Where(c => c.ClaimId == record.ClaimId)
                                                                 .FirstOrDefaultAsync(cancellationToken);
                                if (claim != null)
                                {
                                    amountUsed = amountUsed + claim.ClaimSkus.Select(c => c.TotalAmount).Sum();
                                }
                            }
                        }

                        var claimTotal = await _context.ClaimSkus.Where(c => c.ClaimId == command.Claim.ClaimId)
                                                                 .Select(c => c.TotalAmount)
                                                                 .DefaultIfEmpty()
                                                                 .SumAsync(cancellationToken);

                        amountAvailable = claimFloat.Amount - amountUsed;

                        if (claimTotal > amountAvailable)
                        {
                            //Do not save
                            throw new ClaimException("Claim Number : " + command.Claim.ClaimNumber + " Total Exceeds the available Float Amount. \n\n It can't be approved right now.");
                        }

                        amountAvailable = amountAvailable - claimTotal;
                        if (amountAvailable < claimFloat.MinimumAmount)
                        {
                            //Send warning email
                            string contactPerson = "Sir/Mam";
                            string emailAddress = _claimSettings.DefaultStoreClaimPaymentEmailAddress;

                            var ccEmailAddresses = await _context.ClaimNotificationUsers
                            .Include(c => c.User)
                                    .Where(c => c.ClaimStatusId == (int)ClaimStatusId.Paid && c.EngageRegionId == claimFloat.EngageRegionId)
                                    .Select(c => c.User.Email)
                                    .ToListAsync();

                            if (ccEmailAddresses != null)
                            {
                                var emailInCCemails = ccEmailAddresses.Where(email => email == emailAddress).ToList();
                                if (emailInCCemails != null)
                                {
                                    foreach (var email in emailInCCemails)
                                    {
                                        ccEmailAddresses.Remove(email);
                                    }
                                }
                            }

                            //var templateVariables = new ClaimTemplateVariables
                            //{
                            //    Name = contactPerson,
                            //    SupplierName = claimFloat.Supplier.Name,
                            //    Title = claimFloat.Title,
                            //    TotalAmount = Math.Round(claimFloat.MinimumAmount, 2),
                            //};

                            //await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
                            //{
                            //    EmailTypeId = EmailTypeId.ClaimFloatWarning,
                            //    ToEmail = emailAddress,
                            //    CCEmails = ccEmailAddresses,
                            //    Subject = $"Engage Claim Float Warning - {claimFloat.Supplier.Name}",
                            //    TemplateVariables = templateVariables,
                            //    IsSmtp = false,
                            //    EmailBody = EmailBody.GetClaimFloatWarningBody(contactPerson, claimFloat.Title, Math.Round(claimFloat.MinimumAmount, 2)),
                            //}, cancellationToken);
                            var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.ClaimFloatWarning &&
                                                                                            c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                                                .FirstOrDefaultAsync(cancellationToken);
                            if (template != null)
                            {
                                var templateData = new
                                {
                                    Name = contactPerson,
                                    SupplierName = claimFloat.Supplier.Name,
                                    Title = claimFloat.Title,
                                    TotalAmount = Math.Round(claimFloat.MinimumAmount, 2),
                                };
                                //Save History
                                await _mediator.Send(new CommunicationHistoryClaimFloatInsertCommand
                                {
                                    ClaimFloatId = command.Claim.ClaimFloatId.Value,
                                    CommunicationTemplateId = template.CommunicationTemplateId,
                                    ToEmail = emailAddress,
                                    FromEmail = template.FromEmailAddress,
                                    FromName = template.FromName,
                                    Subject = template.Subject,
                                    Body = template.Body,
                                    CcEmails = ccEmailAddresses.Count > 0 ? string.Join(", ", ccEmailAddresses) : null,
                                    TemplateData = templateData,
                                }, cancellationToken);

                                //Send Email
                                await _mediator.Send(new SendEmailCommand
                                {
                                    ToEmailAddress = emailAddress,
                                    FromEmailAddress = template.FromEmailAddress,
                                    FromEmailName = template.FromName,
                                    CcEmailAddresses = ccEmailAddresses,
                                    Subject = template.Subject,
                                    Body = template.Body,
                                    TemplateData = templateData,
                                }, cancellationToken);
                            }
                        }

                        var claimFloatClaim = new ClaimFloatClaim { ClaimFloatId = claimFloat.ClaimFloatId, ClaimId = command.Claim.ClaimId };
                        _context.ClaimFloatClaims.Add(claimFloatClaim);
                    }
                    else
                    {
                        var existingClaimFloatClaim = await _context.ClaimFloatClaims
                                                                            .Where(c => c.ClaimFloatId == claimFloat.ClaimFloatId
                                                                                && c.ClaimId == command.Claim.ClaimId)
                                                                            .FirstOrDefaultAsync();

                        if (existingClaimFloatClaim != null)
                        {
                            _context.ClaimFloatClaims.Remove(existingClaimFloatClaim);
                        }
                    }
                }
            }
        }

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            return opStatus;
        }

        return new OperationStatus
        {
            Status = true
        };
    }
}
