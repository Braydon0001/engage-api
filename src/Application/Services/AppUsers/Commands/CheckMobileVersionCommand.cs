using Microsoft.Extensions.Configuration;

namespace Engage.Application.Services.AppUsers.Commands;

public class CheckMobileVersionCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public string AppVersion { get; set; }
}

public class CheckMobileVersionCommandHandler : IRequestHandler<CheckMobileVersionCommand, OperationStatus>
{

    protected readonly IAppDbContext _context;
    protected readonly IConfiguration _config;
    public CheckMobileVersionCommandHandler(IAppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<OperationStatus> Handle(CheckMobileVersionCommand request, CancellationToken cancellationToken)
    {
        // fetch the user.
        var user = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == request.EmployeeId, cancellationToken);

        if (user == null)
            return new OperationStatus { Status = false, Message = "Employee not found", ReturnObject = "EmployeeNotFound" };

        // get the latest version of the app from the config file.
        var latestVersion = _config.GetSection("MobileApp").GetSection("Version").Value;

        if (string.IsNullOrEmpty(latestVersion))
            return new OperationStatus { Status = false, Message = "Latest version not found", ReturnObject = "VersionNotFound" };

        // Add version if it doesnt exist
        if (string.IsNullOrEmpty(user.MobileAppVersion))
        {
            user.MobileAppVersion = request.AppVersion;

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            if (opStatus.Status == false) return opStatus;
        }

        if (request.AppVersion == latestVersion)
            return new OperationStatus { Status = true };

        return new OperationStatus { Status = false, Message = "Version mismatch", ReturnObject = "Mismatch" };

    }
}
