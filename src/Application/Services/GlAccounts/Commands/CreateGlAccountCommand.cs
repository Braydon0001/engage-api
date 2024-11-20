namespace Engage.Application.Services.GLAccounts.Commands
{
    public class CreateGLAccountCommand : GLAccountCommand, IRequest<OperationStatus>
    {
    }

    public class CreateGLAccountCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateGLAccountCommand, OperationStatus>
    {
        public CreateGLAccountCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(CreateGLAccountCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateGLAccountCommand, GLAccount>(command);
            _context.GLAccounts.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.GLAccountId;
            return opStatus;
        }
    }
}
