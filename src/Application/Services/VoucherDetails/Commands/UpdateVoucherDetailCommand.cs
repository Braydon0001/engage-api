using Engage.Application.Services.Vat.Models;
using Microsoft.EntityFrameworkCore;

namespace Engage.Application.Services.VoucherDetails.Commands
{
    // Commands
    public class UpdateVoucherDetailCommand : VoucherDetailCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateVoucherDetailAmountCommand : IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdateVoucherDetailNoteCommand : IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public string Note { get; set; }
    }

    public class UpdateVoucherDetailEmployeeCommand : IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
    }

    // Handlers

    public class UpdateVoucherDetailCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateVoucherDetailCommand, OperationStatus>
    {
        private readonly IUserService _user;
        public UpdateVoucherDetailCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
        {
            _user = user;
        }

        public async Task<OperationStatus> Handle(UpdateVoucherDetailCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.VoucherDetails.FirstOrDefaultAsync(x => x.VoucherDetailId == command.Id);

            if (entity.VoucherDetailStatusId == (int)VoucherDetailStatusId.Issued)
            {
                throw new ClaimException("This Voucher is Closed. \n\n It can't be updated right now.");
            }

            entity.VoucherDetailStatusId = (int)VoucherDetailStatusId.Assigned;
            entity.AssignedDate = DateTime.Now;
            entity.AssignedBy = _user.UserName;
            return await SaveChangesAsync(command, entity, nameof(VoucherDetails), command.Id, cancellationToken);
        }
    }

    public class UpdateVoucherDetailAmountCommandHandler : IRequestHandler<UpdateVoucherDetailAmountCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public UpdateVoucherDetailAmountCommandHandler(IAppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<OperationStatus> Handle(UpdateVoucherDetailAmountCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.VoucherDetails.SingleAsync(x => x.VoucherDetailId == command.Id, cancellationToken);

                if (entity.VoucherDetailStatusId == (int)VoucherDetailStatusId.Issued)
                {
                    throw new ClaimException("This Voucher is Closed. \n\n It can't be updated right now.");
                }

                var voucher = await _context.Vouchers.Include(v => v.VoucherDetails)
                                                 .SingleAsync(x => x.VoucherId == entity.VoucherId, cancellationToken);
                if (voucher == null)
                {
                    throw new NotFoundException(nameof(Voucher), entity.VoucherId);
                }

                var usedTotal = voucher.VoucherDetails
                                       .Where(d => d.Disabled == false && d.Deleted == false && d.VoucherDetailId != entity.VoucherDetailId)
                                       .Select(d => d.Amount)
                                       .DefaultIfEmpty().Sum() + command.Amount;

                if (usedTotal > voucher.Total)
                {
                    throw new ClaimException("Voucher Details Total Exceeds the Captured Voucher Total. \n\n It can't be updated right now.");
                }

                entity.Amount = command.Amount;

                var opStatus = await _context.SaveChangesAsync(cancellationToken);
                opStatus.OperationId = entity.VoucherId;
                opStatus.ReturnObject = new VatAmountDto
                {
                    Amount = entity.Amount
                };

                return opStatus;

            }
            catch (VatException ex)
            {
                return OperationStatus.CreateFromException(ex.Message, ex);
            }
        }
    }

    public class UpdateVoucherDetailNoteCommandHandler : IRequestHandler<UpdateVoucherDetailNoteCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;

        public UpdateVoucherDetailNoteCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationStatus> Handle(UpdateVoucherDetailNoteCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.VoucherDetails.SingleAsync(x => x.VoucherDetailId == command.Id);

            if (entity.VoucherDetailStatusId == (int)VoucherDetailStatusId.Issued)
            {
                throw new ClaimException("This Voucher is Closed. \n\n It can't be updated right now.");
            }

            entity.Note = command.Note;

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ClaimId;
            return opStatus;
        }
    }

    public class UpdateVoucherDetailEmployeeCommandHandler : IRequestHandler<UpdateVoucherDetailEmployeeCommand, OperationStatus>
    {
        IUserService _user;
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public UpdateVoucherDetailEmployeeCommandHandler(IAppDbContext context, IMediator mediator, IUserService userService)
        {
            _user = userService;
            _context = context;
            _mediator = mediator;
        }

        public async Task<OperationStatus> Handle(UpdateVoucherDetailEmployeeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.VoucherDetails.SingleAsync(x => x.VoucherDetailId == command.Id, cancellationToken);
            
            if (entity.VoucherDetailStatusId == (int)VoucherDetailStatusId.Issued)
            {
                throw new ClaimException("This Voucher is Closed. \n\n It can't be updated right now.");
            }

            entity.EmployeeId = command.EmployeeId;
            entity.VoucherDetailStatusId = (int)VoucherDetailStatusId.Assigned;
            entity.AssignedDate = DateTime.UtcNow;
            entity.AssignedBy = _user.UserName;

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
