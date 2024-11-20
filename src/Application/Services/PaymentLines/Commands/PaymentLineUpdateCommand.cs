using Engage.Application.Services.Vat.Queries;

namespace Engage.Application.Services.PaymentLines.Commands;

public class PaymentLineUpdateCommand : IMapTo<PaymentLine>, IRequest<PaymentLine>
{
    public int Id { get; set; }
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
        profile.CreateMap<PaymentLineUpdateCommand, PaymentLine>();
    }
}

public record PaymentLineUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IFileService File) : IRequestHandler<PaymentLineUpdateCommand, PaymentLine>
{
    public async Task<PaymentLine> Handle(PaymentLineUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentLines.SingleOrDefaultAsync(e => e.PaymentLineId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var vatAmountQuery = new VatAmountQuery
        {
            VatId = command.IsVat ? (int)VatId.Standard : (int)VatId.ZeroRated,
            Amount = (decimal)command.Amount
        };

        var vatAmount = await Mediator.Send(vatAmountQuery);
        entity.VatAmount = (float)vatAmount;

        var mappedEntity = Mapper.Map(command, entity);

        var existingEmployees = await Context.PaymentLineEmployees.Where(e => e.PaymentLineId == entity.PaymentLineId)
                                                                  .ToListAsync(cancellationToken);

        //get employees to delete and delete them
        var employeesToDelete = existingEmployees.Where(e => !command.EmployeeIds.Contains(e.EmployeeId))
                                                 .ToList();

        Context.PaymentLineEmployees.RemoveRange(employeesToDelete);

        //get employees to add and add them
        var existingEmployeeIds = existingEmployees.Select(e => e.EmployeeId).ToList();

        var employeeIdsToAdd = command.EmployeeIds.Where(id => !existingEmployeeIds.Contains(id)).ToList();

        foreach (var employeeId in employeeIdsToAdd)
        {
            Context.PaymentLineEmployees.Add(new PaymentLineEmployee
            {
                EmployeeId = employeeId,
                PaymentLineId = entity.PaymentLineId
            });
        }

        var existingDivisions = await Context.PaymentLineDivisions.Where(e => e.PaymentLineId == entity.PaymentLineId)
                                                                  .ToListAsync(cancellationToken);

        //get divisions to delete and delete them
        var divisionsToDelete = existingDivisions.Where(e => !command.EmployeeDivisionIds.Contains(e.EmployeeDivisionId))
                                                 .ToList();

        Context.PaymentLineDivisions.RemoveRange(divisionsToDelete);

        //get divisions to add and add them
        var existingDivisionIds = existingDivisions.Select(e => e.EmployeeDivisionId).ToList();

        var divisionIdsToAdd = command.EmployeeDivisionIds.Where(id => !existingDivisionIds.Contains(id)).ToList();

        foreach (var divisionId in divisionIdsToAdd)
        {
            Context.PaymentLineDivisions.Add(new PaymentLineDivision
            {
                EmployeeDivisionId = divisionId,
                PaymentLineId = entity.PaymentLineId
            });
        }

        var existingCostCenters = await Context.PaymentLineCostCenters.Where(e => e.PaymentLineId == entity.PaymentLineId)
                                                                      .ToListAsync(cancellationToken);

        //get cost centers to delete and delete them
        var costCentersToDelete = existingCostCenters.Where(e => !command.CostCenterIds.Contains(e.CostCenterId))
                                                     .ToList();

        Context.PaymentLineCostCenters.RemoveRange(costCentersToDelete);

        //get cost centers to add and add them
        var existingCostCenterIds = existingCostCenters.Select(e => e.CostCenterId).ToList();

        var costCenterIdsToAdd = command.CostCenterIds.Where(id => !existingCostCenterIds.Contains(id)).ToList();

        foreach (var costCenterId in costCenterIdsToAdd)
        {
            Context.PaymentLineCostCenters.Add(new PaymentLineCostCenter
            {
                CostCenterId = costCenterId,
                PaymentLineId = entity.PaymentLineId
            });
        }






        var existingSubDepartments = await Context.PaymentLineCostSubDepartments.Where(e => e.PaymentLineId == entity.PaymentLineId)
                                                                                .ToListAsync(cancellationToken);

        //get sub departments to delete and delete them
        var subDepartmentsToDelete = existingSubDepartments.Where(e => !command.CostSubDepartmentIds.Contains(e.CostSubDepartmentId))
                                                           .ToList();

        Context.PaymentLineCostSubDepartments.RemoveRange(subDepartmentsToDelete);

        //get sub departments to add and add them
        var existingSubDepartmentIds = existingSubDepartments.Select(e => e.CostSubDepartmentId).ToList();

        var subDepartmentIdsToAdd = command.CostSubDepartmentIds.Where(id => !existingSubDepartmentIds.Contains(id)).ToList();

        foreach (var subDeptId in subDepartmentIdsToAdd)
        {
            Context.PaymentLineCostSubDepartments.Add(new PaymentLineCostSubDepartment
            {
                CostSubDepartmentId = subDeptId,
                PaymentLineId = entity.PaymentLineId
            });
        }

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

        return mappedEntity;
    }
}

public class UpdatePaymentLineValidator : AbstractValidator<PaymentLineUpdateCommand>
{
    public UpdatePaymentLineValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ExpenseTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Amount).GreaterThan(0);
    }
}