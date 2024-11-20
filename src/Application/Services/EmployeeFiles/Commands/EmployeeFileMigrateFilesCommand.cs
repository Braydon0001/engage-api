namespace Engage.Application.Services.EmployeeFiles.Commands;

public class EmployeeFileMigrateFilesCommand : IRequest<OperationStatus>
{
}

public record EmployeeFileMigrateFilesHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeFileMigrateFilesCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(EmployeeFileMigrateFilesCommand request, CancellationToken cancellationToken)
    {
        var employees = await Context.Employees.Where(e => e.Disabled == false && e.Deleted == false && e.Files != null).ToListAsync(cancellationToken);

        var fileTypes = await Context.EmployeeFileTypes.ToListAsync(cancellationToken);

        int counter = 0;
        if (employees.Count > 0)
        {
            foreach (var record in employees)
            {
                if (record.Files != null)
                {
                    foreach (var file in record.Files)
                    {
                        if (file.Type != null)
                        {
                            var type = fileTypes.FirstOrDefault(e => e.Name.ToLower() == file.Type.ToLower());
                            if (type != null)
                            {
                                var employeeFile = new EmployeeFile
                                {
                                    EmployeeId = record.EmployeeId,
                                    EmployeeFileTypeId = type.EmployeeFileTypeId,
                                    Files = new List<JsonFile>
                                    {
                                        new JsonFile
                                        {
                                            Name = file.Name,
                                            Type = file.Type,
                                            Url = file.Url,
                                        }
                                    }
                                };
                                Context.EmployeeFiles.Add(employeeFile);
                                counter++;
                            }
                        }
                    }
                }
            }
            if (counter > 0)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        return new OperationStatus { Status = true };
    }
}
