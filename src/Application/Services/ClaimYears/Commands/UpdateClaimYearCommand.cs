namespace Engage.Application.Services.ClaimYears.Commands
{
    public class UpdateClaimYearCommand : ClaimYearCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateClaimYearCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimYearCommand, OperationStatus>
    {
        public UpdateClaimYearCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(UpdateClaimYearCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimYears.SingleAsync(x => x.ClaimYearId == command.Id, cancellationToken);
            _mapper.Map(command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ClaimYearId;
            return opStatus;
        }
    }
}
