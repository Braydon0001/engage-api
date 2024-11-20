using Engage.Application.Services.ProductTransactions.Commands;
using Engage.Application.Services.ProductTransactions.Queries;
using Engage.WebApi.utils;

namespace Engage.WebApi.Controllers;

public partial class ProductTransactionController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<PaginatedListResult<ProductTransactionDto>>> PaginatedQuery(ProductTransactionPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductTransactionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductTransactionVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("history/productId/{productId}/warehouseId/{warehouseId}")]
    public async Task<ActionResult<ListResult<ProductTransactionByProductWarehouseDto>>> ListByProductWarehouse([FromRoute] ProductTransactionByProductWarehouseQuery query, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(query, cancellationToken);
        return Ok(entity);
    }

    [HttpPost("excel")]
    public async Task<FileStreamResult> GenerateExcelExport([FromBody] ProductTransactionExcelExportQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        var file = ExcelFileGeneratorUtil.GenerateExcelFileStream(result.Count, result.ReportName, result.Data, result.ColumnNames, "Export");

        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        //file.Position = 0;

        return File(file, contentType, fileName);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductTransactionInsertCommand command, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus
        {
            Status = true,
            RecordsAffected = entities.Count,
            ReturnObject = entities
        });
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProductTransactionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProductTransactionFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}