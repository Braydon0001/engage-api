namespace Engage.Application.Services.Vacancies.Commands;

public class CreateVacancyCommand : VacancyCommand, IRequest<OperationStatus>
{

}

public class CreateVacancyCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateVacancyCommand, OperationStatus>
{
    public CreateVacancyCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateVacancyCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateVacancyCommand, Vacancy>(command);
        _context.Vacancies.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.VacancyId;
        return opStatus;
    }
}
