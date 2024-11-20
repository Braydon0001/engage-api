namespace Engage.Application.Services.TrainingFiles.Commands;

public class MoveFilesToNewTableCommand : IRequest<OperationStatus>
{
}

public class MoveFilesToNewTableCommandHandler : BaseCreateCommandHandler, IRequestHandler<MoveFilesToNewTableCommand, OperationStatus>
{

    public MoveFilesToNewTableCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
        base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(MoveFilesToNewTableCommand command, CancellationToken cancellationToken)
    {
        var trainingRecords = await _context.Trainings.Where(e => e.Disabled == false && e.Deleted == false)
                                                .ToListAsync(cancellationToken);

        int counter = 0;
        if (trainingRecords.Count > 0)
        {
            foreach (var record in trainingRecords)
            {
                if (record.Files != null)
                {
                    foreach (var file in record.Files)
                    {
                        if (file.Type != null)
                        {
                            var type = await _context.TrainingFileTypes.Where(e => e.Name.ToLower() == file.Type.ToLower())
                                                                        .FirstOrDefaultAsync(cancellationToken);
                            if (type != null)
                            {
                                var trainingFile = new TrainingFile
                                {
                                    TrainingId = record.TrainingId,
                                    TrainingFileTypeId = type.TrainingFileTypeId,
                                    Files = new List<JsonFile>
                                    {
                                        new JsonFile
                                        {
                                            Name = file.Name,
                                            Type = file.Type,
                                            Url = file.Url,
                                        }
                                    },
                                };
                                _context.TrainingFiles.Add(trainingFile);
                                counter++;
                            }
                        }
                    }
                }
            }

            if (counter > 0)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return new OperationStatus { Status = true };
    }
}
