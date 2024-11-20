using System.Threading.Tasks;
using Engage.Application.Services.Shared.AssignCommands;
using Engage.Domain.Entities;
using MediatR;

namespace Engage.Application.Services.Suppliers.Commands
{
    public static class SupplierAssigns
    {
        public static async Task BatchAssign(IMediator mediator, SupplierCommand command, Supplier entity)
        {
            if (command.SupplierTypeIds != null)
            {
                await mediator.Send(new BatchAssignCommand(
                    AssignDesc.TYPE_SUPPLIER, entity.SupplierId, command.SupplierTypeIds));
            }

            if (command.EngageBrandIds != null)
            {
                await mediator.Send(new BatchAssignCommand(
                    AssignDesc.BRAND_SUPPLIER, entity.SupplierId, command.EngageBrandIds));
            }
            
        }
    }
}
