namespace Engage.Application.Services.SupplierContractDetails.Commands;

public record SupplierContractDetailBulkUpdateCommand(List<SupplierContractDetailUpdateCommand> Updates) : IRequest<OperationStatus>
{

}

public class SupplierContractDetailBulkUpdateHandler(IMediator Mediator, IAppDbContext Context) : IRequestHandler<SupplierContractDetailBulkUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SupplierContractDetailBulkUpdateCommand command, CancellationToken cancellationToken)
    {
        foreach (var update in command.Updates)
        {
            await Mediator.Send(new SupplierContractDetailUpdateCommand
            {
                Id = update.Id,
                SupplierContractId = update.SupplierContractId,
                SupplierContractDetailTypeId = update.SupplierContractDetailTypeId,
                EngageRegionId = update.EngageRegionId,
                Name = update.Name,
                Amount = update.Amount,
                RangeStartAmount = update.RangeStartAmount,
                RangeEndAmount = update.RangeEndAmount,
                GlCode = update.GlCode,
                GlSubCode = update.GlSubCode,
                Note = update.Note,
                Reference1 = update.Reference1,
                SaveChanges = false
            }, cancellationToken);
        }

        await Context.SaveChangesAsync(cancellationToken);
        return new OperationStatus(true);
    }
}



