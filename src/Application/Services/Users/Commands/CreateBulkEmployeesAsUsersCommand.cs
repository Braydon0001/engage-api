using Finbuckle.MultiTenant.Abstractions;
using System.Net.Mail;

namespace Engage.Application.Services.Users.Commands;

public class CreateBulkEmployeesAsUsersCommand : IRequest<OperationStatus>
{
}

public class CreateBulkEmployeesAsUsersCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateBulkEmployeesAsUsersCommand, OperationStatus>
{
    private readonly IMultiTenantContextAccessor<TenantAndSupplierInfo> _multiTenantContextAccessor;

    public CreateBulkEmployeesAsUsersCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IMultiTenantContextAccessor<TenantAndSupplierInfo> multiTenantContextAccessor) :
        base(context, mapper, mediator)
    {
        _multiTenantContextAccessor = multiTenantContextAccessor;
    }

    public Task<OperationStatus> Handle(CreateBulkEmployeesAsUsersCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var users = await _context.Users.Where(e => e.Disabled == false && e.Deleted == false)
        //                                .ToListAsync(cancellationToken);
        //var employees = await _context.Employees.Where(e => string.IsNullOrEmpty(e.EmailAddress1) == false)
        //                                        .ToListAsync(cancellationToken);

        //var usersWithNoMatchingEmployees = users.Where(user =>
        //                                                    !employees
        //                                                        .Any(employee =>
        //                                                               employee.EmailAddress1.Trim().ToLower() == user.Email.Trim().ToLower()))
        //                                        .ToList();

        //foreach (var user in usersWithNoMatchingEmployees)
        //{
        //    //var lastEmployeeUserCode = await _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.User)
        //    //                                                   .OrderByDescending(e => e.EmployeeId)
        //    //                                                   .Select(e => e.Code)
        //    //                                                   .FirstOrDefaultAsync(cancellationToken);

        //    await _mediator.Send(new CreateEmployeeCommand
        //    {
        //        SkipUserCreation = true,
        //        EmployeeTypeId = (int)EmployeeTypeId.User,
        //        Code = "User-" + user.UserId,//GenerateNextCode(lastEmployeeUserCode),
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        DateOfBirth = DateTime.Now,
        //        EmailAddress1 = user.Email.Trim(),
        //        StartingDate = DateTime.Now,
        //        UserId = user.UserId,
        //        EmployeeGenderId = 1,
        //        EmployeeRaceId = 1,
        //        EmployeeTitleId = 1,
        //    }, cancellationToken);
        //}

        //var employees = await _context.Employees.Where(e => e.Disabled == false && e.Deleted == false)
        //                                        .ToListAsync(cancellationToken);

        //var users = await _context.Users.Where(u => u.Disabled == false && u.Deleted == false)
        //                                .Select(u => u.Email.ToLower())
        //                                .ToListAsync(cancellationToken);

        //if (employees.Any())
        //{
        //    var employeesToAddAsUsers = employees.Where(e => IsValidEmail(e.EmailAddress1) && !users.Contains(e.EmailAddress1.ToLower())).Take(60).ToList();

        //    if (employeesToAddAsUsers.Any())
        //    {
        //        foreach (var employee in employeesToAddAsUsers)
        //        {
        //            var createUserOp = await _mediator.Send(new CreateUserCommand
        //            {
        //                FirstName = employee.FirstName,
        //                LastName = employee.LastName,
        //                Email = employee.EmailAddress1,
        //                SupplierId = _multiTenantContextAccessor.MultiTenantContext.TenantInfo.SupplierId ?? 126,
        //                IgnoreOrderProductFilters = false,
        //            });
        //        }
        //    }
        //}

        //return new OperationStatus { Status = true };
    }

    private bool IsValidEmail(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }

    public string GenerateNextCode(string previousCode)
    {
        if (string.IsNullOrEmpty(previousCode))
        {
            return "User-1";
        }

        string[] parts = previousCode.Split('-');
        if (parts.Length == 2 && int.TryParse(parts[1], out int number))
        {
            return $"{parts[0]}-{number + 1}";
        }

        return "User-1";
    }
}
