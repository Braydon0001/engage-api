using EntityFramework.Exceptions.Common;

namespace Engage.Application.Services.Suppliers.Commands
{
    public class CreateSupplierCommand : SupplierCommand, IRequest<OperationStatus>
    { }

    public class CreateSupplierCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSupplierCommand, OperationStatus>
    {
        public CreateSupplierCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
            base(context, mapper, mediator)
        { }

        public async Task<OperationStatus> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<CreateSupplierCommand, Supplier>(command);
                var stakeholder = new Stakeholder
                {
                    StakeholderType = StakeholderTypes.Supplier,
                    Created = DateTime.Now
                };
                entity.Stakeholder = stakeholder;
                _context.Suppliers.Add(entity);

                var opStatus = await _context.SaveChangesAsync(cancellationToken);

                if (opStatus.Status == true)
                {
                    opStatus.OperationId = entity.SupplierId;

                    await SupplierAssigns.BatchAssign(_mediator, command, entity);

                    stakeholder.SupplierId = entity.SupplierId;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return opStatus;

            }
            catch (UniqueConstraintException)
            {
                return OperationStatus.CreateUniqueConstraintException("The Supplier Code has already been used by another Supplier. \n Please use another code.");
            }
        }
    }
}
