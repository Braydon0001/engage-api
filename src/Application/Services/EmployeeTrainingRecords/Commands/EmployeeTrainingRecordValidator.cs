namespace Engage.Application.Services.EmployeeTrainingRecords.Commands;

public class EmployeeTrainingRecordValidator<T> : AbstractValidator<T> where T : EmployeeTrainingRecordCommand
{
    public EmployeeTrainingRecordValidator()
    {
        RuleFor(x => x.EmployeeTrainingStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.CourseName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Assessor).MaximumLength(120);
        RuleFor(x => x.Note).MaximumLength(300);
        RuleFor(x => x.Facilitator).MaximumLength(120);
        RuleFor(x => x.CertificateNo).MaximumLength(50);
        RuleFor(x => x.CourseResult).MaximumLength(50);
        RuleFor(x => x.InvoiceNo).MaximumLength(50);
        RuleFor(x => x.StartDate).NotEmpty();
    }
}

public class CreateEmployeeTrainingRecordValidator : EmployeeTrainingRecordValidator<CreateEmployeeTrainingRecordCommand>
{
    public CreateEmployeeTrainingRecordValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeTrainingRecordValidator : EmployeeTrainingRecordValidator<UpdateEmployeeTrainingRecordCommand>
{
    public UpdateEmployeeTrainingRecordValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
