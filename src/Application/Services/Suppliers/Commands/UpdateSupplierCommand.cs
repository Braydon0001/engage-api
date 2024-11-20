namespace Engage.Application.Services.Suppliers.Commands
{
    public class UpdateSupplierCommand : SupplierCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateSupplierCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSupplierCommand, OperationStatus>
    {
        public UpdateSupplierCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
            base(context, mapper, mediator)
        { }

        public async Task<OperationStatus> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Suppliers
                .FirstOrDefaultAsync(x => x.SupplierId == command.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Supplier), command.Id);

            _mapper.Map(command, entity);

            await SupplierAssigns.BatchAssign(_mediator, command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }
    }
}
