using Engage.Application.Services.CommunicationHistoryClaims.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.Claims.Commands;

public class DeleteClaimCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;
    private readonly IEmailService _email;
    private readonly FeatureSwitchOptions _featureSwitch;

    public DeleteClaimCommandHandler(IAppDbContext context, IMediator mediator, IUserService user, IDateTimeService dateTime, IEmailService emailService, IOptions<FeatureSwitchOptions> featureSwitch)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
        _dateTime = dateTime;
        _email = emailService;
        _featureSwitch = featureSwitch.Value;
    }

    public async Task<OperationStatus> Handle(DeleteClaimCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Claims.Include(e => e.Store).SingleOrDefaultAsync(x => x.ClaimId == command.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Claim), command.Id);
        }

        if (entity.ClaimStatusId != (int)ClaimStatusId.Paid)
        {
            var claimHistoryHeader = new ClaimHistoryHeader
            {
                ClaimStatusId = (int)ClaimStatusId.Rejected,
                ClaimClassificationId = entity.ClaimClassificationId,
                EngageRegionId = entity.Store.EngageRegionId,
            };

            var updateStatusCommand = new UpdateClaimStatusCommand
            {
                Id = command.Id,
                ClaimStatusId = (int)ClaimStatusId.Rejected,
                RejectedReason = "Deleted From The History Screen",
                ClaimHistoryHeader = claimHistoryHeader,
                SaveChanges = false,
            };

            await _mediator.Send(updateStatusCommand, cancellationToken);
        }

        var creator = await _context.Users.SingleOrDefaultAsync(x => x.Email == entity.CreatedBy, cancellationToken);
        var creatorName = creator.FullName ?? entity.CreatedBy;

        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == _user.UserName, cancellationToken);
        var userName = user.FullName ?? _user.UserName;

        var ccEmails = !string.IsNullOrEmpty(entity.ApprovedBy) ? new List<string>() { entity.ApprovedBy } : new List<string>();

        if (ccEmails != null)
        {
            var emailInCCemails = ccEmails.SingleOrDefault(email => email == entity.CreatedBy);
            if (emailInCCemails != null)
            {
                ccEmails.Remove(emailInCCemails);
            }
        }

        var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.ClaimRejected &&
                                                                        c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                            .FirstOrDefaultAsync(cancellationToken);
        if (template != null)
        {
            var templateData = new
            {
                ClaimNumber = entity.ClaimNumber,
                ApproverName = userName,
                Name = creatorName,
                RejectedReason = "Deleted From The History Screen",
            };
            //Save History
            await _mediator.Send(new CommunicationHistoryClaimInsertCommand
            {
                ClaimId = command.Id,
                CommunicationTemplateId = template.CommunicationTemplateId,
                ToEmail = entity.CreatedBy,
                FromEmail = template.FromEmailAddress,
                FromName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                CcEmails = ccEmails.Count > 0 ? string.Join(", ", ccEmails) : null,
                TemplateData = templateData,
            }, cancellationToken);

            //Send Email
            await _mediator.Send(new SendEmailCommand
            {
                ToEmailAddress = entity.CreatedBy,
                FromEmailAddress = template.FromEmailAddress,
                FromEmailName = template.FromName,
                CcEmailAddresses = ccEmails,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
            }, cancellationToken);
        }

        //    if (_featureSwitch.NewRejectClaimEmail)
        //    {
        //        var templateVariables = new ClaimTemplateVariables
        //        {
        //            Name = creatorName,
        //            ApproverName = userName,
        //            ClaimNumber = entity.ClaimNumber,
        //            RejectedReason = "Deleted From The History Screen"
        //        };

        //        await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
        //        {
        //            EmailTypeId = EmailTypeId.ClaimRejected,
        //            ToEmail = entity.CreatedBy,
        //            CCEmails = ccEmails,
        //            Subject = $"Engage Deleted Claim Notification - Claim No. {entity.ClaimNumber}",
        //            TemplateVariables = templateVariables,
        //            IsSmtp = false,
        //            EmailBody = EmailBody.GetRejectedClaimsBody(creatorName, userName, entity.ClaimNumber, "Deleted From The History Screen"),
        //        }, cancellationToken);
        //    }
        //    else
        //    {
        //        var deletedClaimEmailCommand = new RejectedClaimEmailCommand
        //        {
        //            Name = creator != null ? creator.FullName : entity.CreatedBy, //Get User's First Name
        //            EmailAddress = entity.CreatedBy,
        //            ClaimNumber = entity.ClaimNumber,
        //            Subject = "Engage Deleted Claim Notification - Claim No. " + entity.ClaimNumber,
        //            CcEmailAddresses = ccEmails, //No need to cc users, approver gets the email
        //            ApproverName = user != null ? user.FullName : _user.UserName, //Get Approver's First Name
        //            RejectedReason = "Deleted From The History Screen",
        //        };

        //        await _mediator.Send(deletedClaimEmailCommand, cancellationToken);
        //    }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.Id;
        return opStatus;
    }
}