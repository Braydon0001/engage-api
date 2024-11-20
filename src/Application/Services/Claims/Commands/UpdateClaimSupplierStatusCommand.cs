using Engage.Application.Services.ClaimHistories.Commands;
using Engage.Application.Services.Claims.Rules.Models;
using Engage.Application.Services.CommunicationHistoryClaims.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.Claims.Commands;

public class UpdateClaimSupplierStatusCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int ClaimSupplierStatusId { get; set; }
    public int? ClaimPendingReasonId { get; set; }
    public string PendingReason { get; set; }
    public ClaimHistoryHeader ClaimHistoryHeader { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class UpdateClaimSupplierStatusCommandHandler : IRequestHandler<UpdateClaimSupplierStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;
    private readonly IEmailService _email;
    private readonly FeatureSwitchOptions _featureSwitch;

    public UpdateClaimSupplierStatusCommandHandler(IAppDbContext context, IMediator mediator, IUserService user, IDateTimeService dateTime, IEmailService emailService, IOptions<FeatureSwitchOptions> featureSwitch)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
        _dateTime = dateTime;
        _email = emailService;
        _featureSwitch = featureSwitch.Value;
    }

    public async Task<OperationStatus> Handle(UpdateClaimSupplierStatusCommand command, CancellationToken cancellationToken)
    {
        var claim = await _context.Claims.SingleAsync(x => x.ClaimId == command.Id, cancellationToken);

        var claimRulecontext = new ClaimRuleContext(claim, _context, _user);

        var canUpdate = await ClaimRuleEngine.CanUpdateSupplierStatus(command.ClaimSupplierStatusId, claimRulecontext, cancellationToken);
        if (!canUpdate.IsSuccess)
        {
            throw new ClaimException(canUpdate.FailureText);
        }
        claim.ClaimSupplierStatusId = command.ClaimSupplierStatusId;

        UpdateHistoryProperties(command, claim);

        await _mediator.Send(new CreateClaimHistoryCommand(claim, command.ClaimHistoryHeader, false), cancellationToken);

        if (command.ClaimSupplierStatusId == (int)ClaimSupplierStatusId.Disputed)
        {
            var store = await _context.Stores.SingleAsync(x => x.StoreId == claim.StoreId, cancellationToken);
            var creator = await _context.Users.SingleOrDefaultAsync(x => x.Email == claim.CreatedBy, cancellationToken);
            var approver = await _context.Users.SingleOrDefaultAsync(x => x.Email == _user.UserName, cancellationToken);

            var ccEmailAddresses = await _context.ClaimNotificationUsers
                            .Include(c => c.User)
                            .Where(c => c.ClaimStatusId == (int)ClaimStatusId.Rejected && c.EngageRegionId == store.EngageRegionId)
                            .Select(c => c.User.Email)
                            .ToListAsync();

            var list = new List<string>();
            if (ccEmailAddresses != null)
            {
                ccEmailAddresses.Add(claim.ApprovedBy);
            }
            else
            {
                list.Add(claim.ApprovedBy);
            }

            if (ccEmailAddresses != null)
            {
                var emailInCCemails = ccEmailAddresses.Where(email => email == claim.CreatedBy).ToList();
                if (emailInCCemails != null)
                {
                    foreach (var email in emailInCCemails)
                    {
                        ccEmailAddresses.Remove(email);
                    }
                }
            }
            else
            {
                var emailInCCemails = list.Where(email => email == claim.CreatedBy).ToList();
                if (emailInCCemails != null)
                {
                    foreach (var email in emailInCCemails)
                    {
                        ccEmailAddresses.Remove(email);
                    }
                }
            }

            var userName = approver.FullName ?? _user.UserName;

            var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.ClaimDisputed &&
                                                                            c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                                .FirstOrDefaultAsync(cancellationToken);
            if (template != null)
            {
                var templateData = new
                {
                    ClaimNumber = claim.ClaimNumber,
                    ApproverName = userName,
                    Name = creator != null ? creator.FullName : claim.CreatedBy,
                    DisputedReason = command.PendingReason,
                };
                //Save History
                var cc = ccEmailAddresses ?? list;
                await _mediator.Send(new CommunicationHistoryClaimInsertCommand
                {
                    ClaimId = command.Id,
                    CommunicationTemplateId = template.CommunicationTemplateId,
                    ToEmail = claim.CreatedBy,
                    FromEmail = template.FromEmailAddress,
                    FromName = template.FromName,
                    Subject = template.Subject,
                    Body = template.Body,
                    CcEmails = cc.Count > 0 ? string.Join(", ", cc) : null,
                    TemplateData = templateData,
                }, cancellationToken);

                //Send Email
                await _mediator.Send(new SendEmailCommand
                {
                    ToEmailAddress = claim.CreatedBy,
                    FromEmailAddress = template.FromEmailAddress,
                    FromEmailName = template.FromName,
                    CcEmailAddresses = cc,
                    Subject = template.Subject,
                    Body = template.Body,
                    TemplateData = templateData,
                }, cancellationToken);

            }
            //if (_featureSwitch.NewDisputeClaimEmail)
            //{
            //    var templateVariables = new ClaimTemplateVariables
            //    {
            //        Name = creator != null ? creator.FullName : claim.CreatedBy,
            //        ApproverName = userName,
            //        ClaimNumber = claim.ClaimNumber,
            //        DisputedReason = command.PendingReason,
            //    };

            //    await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
            //    {
            //        EmailTypeId = EmailTypeId.ClaimDisputed,
            //        ToEmail = claim.CreatedBy,
            //        CCEmails = ccEmailAddresses != null ? ccEmailAddresses : list,
            //        Subject = $"Engage Disputed Claim Notification - Claim No. {claim.ClaimNumber}",
            //        TemplateVariables = templateVariables,
            //        IsSmtp = false,
            //        EmailBody = EmailBody.GetDisputedClaimsBody(creator != null ? creator.FullName : claim.CreatedBy, userName, claim.ClaimNumber, command.PendingReason),
            //    }, cancellationToken);
            //}
            //else
            //{
            //    var disputedClaimEmailCommand = new DisputedClaimEmailCommand
            //    {
            //        Name = creator != null ? creator.FullName : claim.CreatedBy, //Get User's First Name
            //        EmailAddress = claim.CreatedBy,
            //        ClaimNumber = claim.ClaimNumber,
            //        Subject = "Engage Disputed Claim Notification - Claim No. " + claim.ClaimNumber,
            //        CcEmailAddresses = ccEmailAddresses != null ? ccEmailAddresses : list, //Get CC users for region and Status
            //        ApproverName = approver != null ? approver.FullName : _user.UserName, //Get Approver's First Name
            //        DisputedReason = command.PendingReason,
            //    };

            //    await _mediator.Send(disputedClaimEmailCommand, cancellationToken);
            //}
        }

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }

    private void UpdateHistoryProperties(UpdateClaimSupplierStatusCommand command, Claim claim)
    {
        switch (command.ClaimSupplierStatusId)
        {

            case (int)ClaimSupplierStatusId.Approved:
                claim.SupplierApprovedDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    claim.SupplierApprovedBy = _user.UserName;
                }
                break;
            case (int)ClaimSupplierStatusId.Disputed:
                claim.PendingDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    claim.PendingBy = _user.UserName;
                }
                if (command.ClaimPendingReasonId.HasValue)
                {
                    claim.ClaimPendingReasonId = command.ClaimPendingReasonId.Value;
                }
                if (!string.IsNullOrWhiteSpace(command.PendingReason))
                {
                    claim.PendingReason = command.PendingReason;
                }
                break;
        }
    }
}