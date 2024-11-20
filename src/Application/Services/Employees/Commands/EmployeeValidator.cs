namespace Engage.Application.Services.Employees.Commands;

public class EmployeeValidator<T> : AbstractValidator<T> where T : EmployeeCommand
{
    public EmployeeValidator()
    {
        RuleFor(x => x.EmployeeStateId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.EmployeeTitleId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FirstName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.MiddleName).MaximumLength(120);
        RuleFor(x => x.LastName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Initials).MaximumLength(10);
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.IdNumber).MaximumLength(30);
        RuleFor(x => x.PassportNumber).MaximumLength(30);
        RuleFor(x => x.EmployeeNationalityId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeCitzenshipId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeRaceId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeGenderId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.MaritalStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PAYENumber).MaximumLength(15);
        RuleFor(x => x.SARSNumber).MaximumLength(15);
        RuleFor(x => x.UIFNumber).MaximumLength(15);
        RuleFor(x => x.RANumber).MaximumLength(15);
        RuleFor(x => x.MedicalAidNumber).MaximumLength(15);
        RuleFor(x => x.StartingDate).NotEmpty();
        RuleFor(x => x.LeaveAccumulationRate).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AnnualLeave).GreaterThanOrEqualTo(0);
        RuleFor(x => x.SickLeave).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FamilyLeave).GreaterThanOrEqualTo(0);
        RuleFor(x => x.EmailAddress1).MaximumLength(100);
        RuleFor(x => x.EmailAddress2).MaximumLength(100);
        RuleFor(x => x.PhoneNumber).MaximumLength(20);
        RuleFor(x => x.Note).MaximumLength(300);
        RuleForEach(x => x.EngageDepartmentIds).GreaterThan(0);
        RuleForEach(x => x.EngageRegionIds).GreaterThan(0);
    }
}

public class UpdateEmployeeValidator : EmployeeValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
