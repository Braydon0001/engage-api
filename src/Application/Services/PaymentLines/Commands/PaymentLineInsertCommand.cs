using Engage.Application.Services.Vat.Queries;

namespace Engage.Application.Services.PaymentLines.Commands;

public class PaymentLineInsertCommand : IMapTo<PaymentLine>, IRequest<PaymentLine>
{
    public int PaymentId { get; init; }
    public int ExpenseTypeId { get; init; }
    public List<int> EmployeeIds { get; init; }
    public List<int> EmployeeDivisionIds { get; init; }
    public List<int> CostCenterIds { get; init; }
    public List<int> CostSubDepartmentIds { get; init; }
    public bool IsVat { get; init; }
    public bool IsSplitAmount { get; init; }
    public bool HasQuote { get; init; }
    public bool HasInvoice { get; init; }
    public float Amount { get; init; }
    public IFormFile[] FilesQuote { get; set; }
    public IFormFile[] FilesInvoice { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineInsertCommand, PaymentLine>();
    }
}

public record PaymentLineInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IFileService File) : IRequestHandler<PaymentLineInsertCommand, PaymentLine>
{
    public async Task<PaymentLine> Handle(PaymentLineInsertCommand command, CancellationToken cancellationToken)
    {

        var entity = Mapper.Map<PaymentLineInsertCommand, PaymentLine>(command);

        var vatAmountQuery = new VatAmountQuery
        {
            VatId = command.IsVat ? (int)VatId.Standard : (int)VatId.ZeroRated,
            Amount = (decimal)command.Amount
        };

        var vatAmount = await Mediator.Send(vatAmountQuery);
        entity.VatAmount = (float)vatAmount;

        foreach (var employeeId in command.EmployeeIds)
        {
            var newLineEmployee = new PaymentLineEmployee
            {
                PaymentLine = entity,
                EmployeeId = employeeId,
            };

            entity.Employees.Add(newLineEmployee);
        }

        foreach (var divisionId in command.EmployeeDivisionIds)
        {
            var newLineDivision = new PaymentLineDivision
            {
                PaymentLine = entity,
                EmployeeDivisionId = divisionId,
            };

            entity.EmployeeDivisions.Add(newLineDivision);
        }

        foreach (var costCenterId in command.CostCenterIds)
        {
            var newLineCostCenter = new PaymentLineCostCenter
            {
                PaymentLine = entity,
                CostCenterId = costCenterId,
            };

            entity.CostCenters.Add(newLineCostCenter);
        }

        foreach (var costSubDepartmentId in command.CostSubDepartmentIds)
        {
            var newLineSubDept = new PaymentLineCostSubDepartment
            {
                PaymentLine = entity,
                CostSubDepartmentId = costSubDepartmentId,
            };

            entity.SubDepartments.Add(newLineSubDept);
        }

        Context.PaymentLines.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status)
        {
            var quoteFileUpdateCommand = new FileUpdateCommand
            {
                ContainerName = nameof(PaymentLine),
                EntityFiles = entity.Files,
                MaxFiles = 5,
                Files = command.FilesQuote,
                Id = entity.PaymentLineId,
                FileType = "quote"
            };

            entity.Files = await File.UpdateAsync(quoteFileUpdateCommand, cancellationToken);

            var invoiceFileUpdateCommand = new FileUpdateCommand
            {
                ContainerName = nameof(PaymentLine),
                EntityFiles = entity.Files,
                MaxFiles = 5,
                Files = command.FilesInvoice,
                Id = entity.PaymentLineId,
                FileType = "invoice"
            };

            entity.Files = await File.UpdateAsync(invoiceFileUpdateCommand, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }
}

public class PaymentLineInsertValidator : AbstractValidator<PaymentLineInsertCommand>
{
    public PaymentLineInsertValidator()
    {
        RuleFor(e => e.PaymentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ExpenseTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Amount).GreaterThan(0);
    }
}