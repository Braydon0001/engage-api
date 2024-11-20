using Engage.Application.Services.DistributionCenters.Queries;
using Engage.Application.Services.Options.Queries;
using Engage.Application.Services.Orders.Commands;
using Engage.Application.Services.Orders.Models;
using Engage.Application.Services.Orders.Queries;
using Engage.Application.Services.OrderSkus.Commands;
using Engage.Application.Services.OrderSkus.Models;
using Engage.Application.Services.OrderSkus.Queries;
using Engage.Application.Services.Products.Models;
using Engage.Application.Services.Products.Queries;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace Engage.WebApi.Controllers;

[Authorize("order2")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/order")]
public class Order2Controller : Base2Controller
{
    /// <summary>
    /// Get an order with it's product skus.  
    /// Returns an OrderVm.
    /// </summary>
    [HttpGet("{orderId}")]
    [OpenApiTag("Orders")]
    public async Task<ActionResult<OrderVm>> GetOrder([FromRoute] int orderId)
    {
        return Ok(await Mediator.Send(new OrderVmQuery2 { Id = orderId }));
    }

    /// <summary>
    /// Get an order sku.
    /// Returns an OrderSkuDto.
    /// </summary>
    [HttpGet("sku/{orderSkuId}")]
    [OpenApiTag("Order Skus")]
    public async Task<ActionResult<OrderSkuDto>> GetOrderSku([FromRoute][Description("Sku: Stock Keeping Unit also known as an Order Line.")] int orderSkuId)
    {
        return Ok(await Mediator.Send(new GetOrderSkuQuery { Id = orderSkuId }));
    }

    /// <summary>
    /// Get a list of options which are entities with an id and name. 
    /// Can be one of 'DistributionCenters', 'EngageRegions', 'OrderTypes', 'OrderStatuses', 'OrderQuantityTypes', 
    /// Returns a list of OptionDto. 
    /// Returns a 500 error ("Option \"{optionType}\" does not exist.") if an invalid OptionType is specified. 
    /// </summary>        
    [HttpGet("option/type/{optionType}")]
    [OpenApiTag("Order Options")]
    //TODO Validate the optionType with list of allowed values (String Enum) and return a bad request  
    public async Task<ActionResult<List<OptionDto>>> GetOrderTypeOptions([BindRequired][FromRoute] string optionType)
    {
        return Ok(await Mediator.Send(new OptionsQuery { Option = optionType }));
    }

    /// <summary>
    /// Get a filtered list of stores. 
    /// Returns a list of OptionDto. 
    /// </summary>
    [HttpGet("option/store")]
    [OpenApiTag("Order Options")]
    public async Task<ActionResult<List<OptionDto>>> GetOrderStoreOptions(
        [BindRequired][Description("Search for store names that contain the search text.")][FromQuery] string search)
    {
        return Ok(await Mediator.Send(new OrderStoresQuery { Search = search }));
    }

    /// <summary>
    /// Get the distribution centers for a store. 
    /// In general, each store only uses one distribution center.        
    /// Returns a list of DependentOptionDto, where the parentId is the storeId. 
    /// </summary>
    [HttpGet("option/distributioncenter/store/{storeId}")]
    [OpenApiTag("Order Options")]
    public async Task<ActionResult<List<DependentOptionDto>>> GetOrderDistributionCenterOptions([FromRoute] int storeId)
    {
        return Ok(await Mediator.Send(new DistributionCentersByStoreQuery { StoreId = storeId }));
    }

    /// <summary>
    /// Get a filtered list of products.   
    /// Returns a list of OrderSkuProductOptionDto. 
    /// </summary>        
    [HttpGet("option/product/distributioncenter/{distributionCenterId}")]
    [OpenApiTag("Order Options")]
    public async Task<ActionResult<List<ProductOptionDto>>> GetOrderProducts(
        [BindRequired][Description("Search for product codes or names that contain the search text.")][FromQuery] string search,
        [FromRoute] int distributionCenterId)
    {
        return Ok(await Mediator.Send(new ProductsQuery
        {
            Search = search,
            DistributionCenterId = distributionCenterId,
            IsSupplierProductsOnly = true
        }));
    }

    /// <summary>
    /// Creates an order, without any skus, or with one or more skus.  
    /// The request body is a CreateOrderCommand2. 
    /// Returns an OperationResponse.
    /// </summary>        
    [HttpPost]
    [OpenApiTag("Orders")]
    public async Task<ActionResult<OperationResponse>> Create(CreateOrderCommand2 command)
    {
        return Ok(OperationResponse.CreateFromStatus(await Mediator.Send(command)));
    }

    /// <summary>
    /// Creates one, or more, order skus (order lines).  
    /// The request body is an CreateOrderSkuCommand2.
    /// Returns an OperationResponse.
    /// </summary>
    [HttpPost("sku")]
    [OpenApiTag("Order Skus")]
    public async Task<ActionResult<OperationResponse>> CreateSku(CreateOrderSkuCommand2 command)
    {
        return Ok(OperationResponse.CreateFromStatus(await Mediator.Send(command)));
    }

    /// <summary>
    /// Updates an order.  
    /// The request body is an UpdateOrderCommand2. 
    /// Returns an OperationResponse.
    /// </summary>
    [HttpPut]
    [OpenApiTag("Orders")]
    public async Task<ActionResult<OperationResponse>> Update(UpdateOrderCommand2 command)
    {
        return Ok(OperationResponse.CreateFromStatus(await Mediator.Send(command)));
    }

    /// <summary>
    /// Updates an order sku (order line).   
    /// Returns an OperationResponse.
    /// </summary>
    [HttpPut("sku")]
    [OpenApiTag("Order Skus")]
    public async Task<ActionResult<OperationResponse>> UpdateSku(OrderSkuUpdateCommand2 command)
    {
        return Ok(OperationResponse.CreateFromStatus(await Mediator.Send(command)));
    }

    /// <summary>
    /// Soft deletes an order by updating the deleted field to true.     
    /// An order can ony be deleted if it is 'Unsubmitted' or 'Submitted'. 
    /// Returns an OperationResponse.
    /// </summary>
    [HttpDelete("{orderId}")]
    [OpenApiTag("Orders")]
    public async Task<ActionResult<OperationResponse>> Delete(int orderId)
    {
        return Ok(OperationResponse.CreateFromStatus(await Mediator.Send(new DeleteCommand
        {
            EntityName = "order",
            Id = orderId
        })));
    }

    /// <summary>
    /// Soft deletes an order sku (order line) by updating the deleted field to true.
    /// An order sku can ony be deleted if the order is 'Unsubmitted' or 'Submitted'.
    /// Returns an OperationResponse.
    /// </summary>
    [HttpDelete("sku/{orderSkuId}")]
    [OpenApiTag("Order Skus")]
    public async Task<ActionResult<OperationResponse>> DeleteSku(int orderSkuId)
    {
        return Ok(OperationResponse.CreateFromStatus(await Mediator.Send(new DeleteCommand
        {
            EntityName = "orderski",
            Id = orderSkuId
        })));
    }

}
