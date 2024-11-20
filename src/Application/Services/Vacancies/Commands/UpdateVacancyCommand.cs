namespace Engage.Application.Services.Vacancies.Commands
{
    public class UpdateVacancyCommand : VacancyCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateVacancyCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateVacancyCommand, OperationStatus>
    {
        public UpdateVacancyCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(UpdateVacancyCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Vacancies.SingleAsync(x => x.VacancyId == command.Id, cancellationToken);
            _mapper.Map(command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.VacancyId;
            return opStatus;
        }
    }
}
