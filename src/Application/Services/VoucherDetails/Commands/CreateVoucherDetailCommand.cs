using EntityFramework.Exceptions.Common;

namespace Engage.Application.Services.VoucherDetails.Commands;

public class CreateVoucherDetailCommand : VoucherDetailCommand, IRequest<OperationStatus>
{
    public bool SaveChanges { get; set; } = true;
}

public class CreateVoucherDetailCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateVoucherDetailCommand, OperationStatus>
{
    private readonly IUserService _user;
    public CreateVoucherDetailCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<OperationStatus> Handle(CreateVoucherDetailCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var voucher = await _context.Vouchers.Include(v => v.VoucherDetails)
                                                 .SingleAsync(x => x.VoucherId == command.VoucherId, cancellationToken);
            if (voucher == null)
            {
                throw new NotFoundException(nameof(Voucher), command.VoucherId);
            }

            var usedTotal = voucher.VoucherDetails
                                   .Where(d => d.Disabled == false && d.Deleted == false)
                                   .Select(d => d.Amount)
                                   .DefaultIfEmpty().Sum() + command.Amount;

            if (usedTotal > voucher.Total)
            {
                throw new ClaimException("Voucher Details Total Exceeds the Captured Voucher Total. \n\n It can't be added right now.");
            }

            var entity = _mapper.Map<CreateVoucherDetailCommand, VoucherDetail>(command);

            //if an employee was chosen then set voucherDetails to issued
            if (entity.EmployeeId == null)
            {
                entity.VoucherDetailStatusId = (int)VoucherDetailStatusId.Received;
            } else
            {
                entity.VoucherDetailStatusId = (int)VoucherDetailStatusId.Assigned;
                entity.AssignedDate = DateTime.UtcNow;
                entity.AssignedBy = _user.UserName;
            }
            _context.VoucherDetails.Add(entity);

            if (command.SaveChanges)
            {
                var opStatus = await _context.SaveChangesAsync(cancellationToken);
                opStatus.OperationId = entity.VoucherDetailId;
                return opStatus;
            }
            else
            {
                return new OperationStatus
                {
                    Status = true
                };
            }

        }
        catch (UniqueConstraintException)
        {
            return OperationStatus.CreateUniqueConstraintException("This voucher number has already been added to the Voucher. \n It can't be added again.");
        }
    }
}
