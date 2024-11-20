using AutoMapper;
using Engage.Application.Exceptions;
using Engage.Application.Interfaces;
using Engage.Application.Models;
using Engage.Application.Services.Shared.Commands;
using Engage.Application.Services.Vat.Models;
using Engage.Application.Services.Vat.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.ClaimSkus.Commands
{
    // Commands
    public class UpdateClaimSkuCommand : ClaimSkuCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateClaimSkuQuantityTypeCommand : IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public int ClaimQuantityTypeId { get; set; }
    }

    public class UpdateClaimSkuQuantityCommand : IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateClaimSkuAmountCommand : IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int VatId { get; set; }
        public int? ClaimVatId { get; set; }
    }

    public class UpdateClaimSkuNoteCommand : IRequest<OperationStatus>
    {
        public int Id { get; set; }
        public string Note { get; set; }
    }

    // Handlers

    public class UpdateClaimSkuCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimSkuCommand, OperationStatus>
    {
        public UpdateClaimSkuCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(UpdateClaimSkuCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimSkus.FirstOrDefaultAsync(x => x.ClaimSkuId == command.Id);
            return await SaveChangesAsync(command, entity, nameof(ClaimSkus), command.Id, cancellationToken);
        }
    }

    public class UpdateClaimSkuQuantityTypeCommandHandler : IRequestHandler<UpdateClaimSkuQuantityTypeCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;

        public UpdateClaimSkuQuantityTypeCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationStatus> Handle(UpdateClaimSkuQuantityTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimSkus.SingleAsync(x => x.ClaimSkuId == command.Id);
            entity.ClaimQuantityTypeId = command.ClaimQuantityTypeId;

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ClaimId;
            return opStatus;
        }
    }

    public class UpdateClaimSkuQuantityCommandHandler : IRequestHandler<UpdateClaimSkuQuantityCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;

        public UpdateClaimSkuQuantityCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationStatus> Handle(UpdateClaimSkuQuantityCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimSkus.SingleAsync(x => x.ClaimSkuId == command.Id);
            entity.Quantity = command.Quantity;

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ClaimId;
            return opStatus;
        }
    }

    public class UpdateClaimSkuAmountCommandHandler : IRequestHandler<UpdateClaimSkuAmountCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public UpdateClaimSkuAmountCommandHandler(IAppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<OperationStatus> Handle(UpdateClaimSkuAmountCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.ClaimSkus.SingleAsync(x => x.ClaimSkuId == command.Id, cancellationToken);
                entity.Amount = command.Amount;

                var vatAmountQuery = new VatAmountQuery
                {
                    VatId = command.VatId,
                    Amount = command.Amount
                };
                if (command.ClaimVatId.HasValue)
                {
                    vatAmountQuery.OverrideVatId = command.ClaimVatId;
                }

                var vatAmount = await _mediator.Send(vatAmountQuery);
                entity.VatAmount = vatAmount;

                var opStatus = await _context.SaveChangesAsync(cancellationToken);
                opStatus.OperationId = entity.ClaimId;
                opStatus.ReturnObject = new VatAmountDto
                {
                    Amount = entity.Amount,
                    VatAmount = entity.VatAmount,
                    TotalAmount = entity.TotalAmount
                };
                return opStatus;

            }
            catch (VatException ex)
            {
                return OperationStatus.CreateFromException(ex.Message, ex);
            }
        }
    }

    public class UpdateClaimSkuNoteCommandHandler : IRequestHandler<UpdateClaimSkuNoteCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;

        public UpdateClaimSkuNoteCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationStatus> Handle(UpdateClaimSkuNoteCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimSkus.SingleAsync(x => x.ClaimSkuId == command.Id);
            entity.Note = command.Note;

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ClaimId;
            return opStatus;
        }
    }
}
