namespace Engage.Application.Services.EmployeeEmployeeKpis.Commands;

public class EmployeeEmployeeKpiUpdateCommand : IMapTo<EmployeeEmployeeKpi>, IRequest<EmployeeEmployeeKpi>//EmployeeEmployeeKpiCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public int EmployeeKpiId { get; set; }
    //public int? EmployeeKpiTierId { get; set; }
    public int Score { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class EmployeeEmployeeKpiUpdateCommandHandler : UpdateHandler, IRequestHandler<EmployeeEmployeeKpiUpdateCommand, EmployeeEmployeeKpi>
{
    public EmployeeEmployeeKpiUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeEmployeeKpi> Handle(EmployeeEmployeeKpiUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeEmployeeKpis.SingleAsync(x => x.EmployeeId == command.EmployeeId && x.EmployeeKpiId == command.EmployeeKpiId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        if (command.SaveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        //var opStatus = await _context.SaveChangesAsync(cancellationToken);

        //return opStatus;
        return mappedEntity;
    }
}
