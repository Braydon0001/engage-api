using Engage.Application.Services.EngageVariantProducts.Models;
using Engage.Application.Services.EngageVariantProducts.Queries;
using Engage.Application.Services.Mobile.Orders.Models;
using Engage.Application.Services.Mobile.Orders.Queries;
using Engage.Application.Services.Options.Queries;
using Engage.Application.Services.Orders.Commands;
using Engage.Application.Services.Orders.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public class OrderController : BaseMobileController
{
    [HttpGet]
    [Route("GetOrderHistoryByUserId/{id}")]
    public async Task<ActionResult<List<MobileOrderDto>>> GetOrderHistoryByUserId(int id, [FromQuery] string StoreSearch)
    {
        return Ok(await Mediator.Send(new GetMobileOrderHistoryQuery() { EmployeeId = id, StoreSearch = StoreSearch }));
    }

    [HttpGet("GetEngageVariantProduct/{distributionCenterId}")]
    public async Task<ActionResult<List<OptionDto>>> GetEngageVariantProduct([FromRoute] EngageVariantProductMobileOptionListQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("GetMobileEngageDcProducts")]
    public async Task<ActionResult<List<OptionDto>>> GetMobileEngageDcProducts([FromRoute] EngageVariantProductMobileOptionList2Query query, [FromQuery] string search, [FromQuery] string dcIds, [FromQuery] string brandIds, [FromQuery] string departmentIds, [FromQuery] string supplierIds, [FromQuery] string productClassificationIds, [FromQuery] string subCategoryIds)
    {
        query.Search = search;
        query.DistributionCenterIds = dcIds.Split(',').Select(int.Parse).ToList();
        query.BrandIds = !string.IsNullOrWhiteSpace(brandIds) ? brandIds.Split(',').Select(int.Parse).ToList() : null;
        query.DepartmentIds = !string.IsNullOrWhiteSpace(departmentIds) ? departmentIds.Split(',').Select(int.Parse).ToList() : null;
        query.SupplierIds = !string.IsNullOrWhiteSpace(supplierIds) ? supplierIds.Split(',').Select(int.Parse).ToList() : null;
        query.ProductClassificationIds = !string.IsNullOrWhiteSpace(productClassificationIds) ? productClassificationIds.Split(',').Select(int.Parse).ToList() : null;
        query.SubCategoryIds = !string.IsNullOrWhiteSpace(subCategoryIds) ? subCategoryIds.Split(',').Select(int.Parse).ToList() : null;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("productsbyemployeesubgroup/{employeeId}")]
    public async Task<ActionResult<List<OptionDto>>> GetEngageVariantProductsByEmployeeSubGroup(int employeeId)
    {
        return Ok(await Mediator.Send(new EngageVariantProductByEmployeeSubGroupOptionQuery
        {
            EmployeeId = employeeId
        }));
    }

    [HttpGet("productsbyemployeesubgrouanddepartment/{employeeId}")]
    public async Task<ActionResult<List<OptionDto>>> VariantProductByEmployeeSubGroupAndDepartmentOption(int employeeId)
    {
        return Ok(await Mediator.Send(new VariantProductByEmployeeSubGroupAndDepartmentOptionQuery
        {
            EmployeeId = employeeId
        }));
    }

    [HttpPost("AddMobileOrder")]
    public async Task<IActionResult> AddMobileOrder(OrderMobileInsertCommand command)
    {
        return Ok(await Mediator.Send(command));

    }
    [HttpPost("AddMobileOrderWithDrops")]
    public async Task<IActionResult> AddMobileOrderWithDrops(OrderMobileInsertWithDeliveryDateCommand command)
    {
        var order = await Mediator.Send(command);

        if (order.OperationId is int && command.EmailAddresses != null && command.EmailAddresses.Count > 0)
        {

            var orderId = (int)order.OperationId;

            var orderTemplateVm = await Mediator.Send(new OrderEmailVmQuery { Id = orderId });

            string sendEmail;

            List<string> emails = command.EmailAddresses.Select(e => e.Value).ToList();
            if (orderTemplateVm.Email == "")
            {
                sendEmail = emails[0];
                emails.RemoveAt(0);
            }
            else
            {
                sendEmail = orderTemplateVm.Email;
            }
            var attachment = await Mediator.Send(new OrderGenerateSummaryPdfCommand { Id = orderId });


            await Mediator.Send(new OrderSubmitEmailCommand
            {
                EmailAddress = sendEmail,
                StoreName = orderTemplateVm.StoreName,
                OrderDate = orderTemplateVm.OrderDate.ToShortDateString().Replace('/', '-'),
                OrderId = orderId,
                CCEmails = emails,
                Attachment = attachment
            });
        }

        return Ok(order);

    }

    [HttpGet("option/{option}")]
    public async Task<ActionResult<List<OptionDto>>> GetByType([FromRoute] OptionsQuery query, [FromQuery] string search, [FromQuery] bool isRegional)
    {
        query.Search = search;
        query.IsRegional = isRegional;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("engageVariantProduct/paginated")]
    public async Task<ActionResult<ListResult<EngageVariantProductCatalogDto>>> GetAll([FromBody] EngageVariantProductCatalogPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}