namespace Engage.Application.Services.TrainingYears.Commands
{
    public class UpdateTrainingYearCommand : TrainingYearCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateTrainingYearCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateTrainingYearCommand, OperationStatus>
    {
        public UpdateTrainingYearCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(UpdateTrainingYearCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.TrainingYears.SingleAsync(x => x.TrainingYearId == command.Id, cancellationToken);
            _mapper.Map(command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.TrainingYearId;
            return opStatus;
        }
    }
}
