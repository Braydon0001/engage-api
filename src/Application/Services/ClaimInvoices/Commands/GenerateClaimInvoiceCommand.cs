using Engage.Application.Services.ClaimInvoices.Models;
using Engage.Application.Services.ClaimReports.Models;
using System.Globalization;

namespace Engage.Application.Services.ClaimInvoices.Commands
{
    public class GenerateClaimInvoiceCommand : GetQuery, IRequest<ReportListVM<InvoiceDto>>
    {
        public int[] ClaimIDs { get; set; }
    }

    public class GenerateClaimInvoiceCommandHandler : BaseQueryHandler, IRequestHandler<GenerateClaimInvoiceCommand, ReportListVM<InvoiceDto>>
    {
        public GenerateClaimInvoiceCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ReportListVM<InvoiceDto>> Handle(GenerateClaimInvoiceCommand command, CancellationToken cancellationToken)
        {
            var firstClaim = await _context.Claims.SingleOrDefaultAsync(e => e.ClaimId == command.ClaimIDs[0]);

            var approvedDate = (DateTime)firstClaim.ApprovedDate;

            var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                        .Where(e => approvedDate.Date >= e.StartDate.Date && approvedDate.Date <= e.EndDate.Date)
                                        .SingleAsync();

            var entities = await _context.ClaimSkus
                                .Include(c => c.Claim)
                                .Include(e => e.Claim.ClaimType)
                                .Include(e => e.Claim.Store)
                                .Include(e => e.Claim.ClaimAccountManager)
                                .Where(e => command.ClaimIDs.Contains(e.ClaimId))
                                .OrderBy(e => e.Claim.Store.Name)
                                .ThenBy(c => c.ClaimId)
                                .ThenBy(c => c.Claim.Created)
                                .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();

            var invNumInvDate = "25/" + claimPeriod.EndDate.Month.ToString() + "/" + claimPeriod.ClaimYear.Name.ToString(); //25/10/2021
            var invNumExtOrderNum = "ENCORE CLAIMS " + claimPeriod.EndDate.ToString("MMM", CultureInfo.InvariantCulture).ToUpper() + claimPeriod.EndDate.ToString("yy"); //ENCORE CLAIMS OCT21

            var data = entities.Select(c => new InvoiceDto
            {
                InvNumDocType = c.InvNumDocType,
                InvNumInvDate = invNumInvDate,
                InvNumAccountId = c.InvNumAccountId,
                BTBLInvoiceLinesILedgerAccountId = c.BTBLInvoiceLinesILedgerAccountId,
                BTBLInvoiceLinesCDescription = c.BTBLInvoiceLinesCDescription,
                BTBLInvoiceLinesFUnitPriceExcl = Math.Round(c.BTBLInvoiceLinesFUnitPriceExcl, 2),
                BTBLInvoiceLinesITaxTypeId = c.BTBLInvoiceLinesITaxTypeId,
                InvNumExtOrderNum = string.IsNullOrEmpty(c.InvNumExtOrderNum) ? "ENCORE CLAIMS " + claimPeriod.EndDate.ToString("MMM", CultureInfo.InvariantCulture).ToUpper() + claimPeriod.EndDate.ToString("yy") : c.InvNumExtOrderNum + " CLAIMS " + claimPeriod.EndDate.ToString("MMM", CultureInfo.InvariantCulture).ToUpper() + claimPeriod.EndDate.ToString("yy"),
                BTBLInvoiceLinesFQuantity = c.BTBLInvoiceLinesFQuantity,
                BTBLInvoiceLinesIModule = c.BTBLInvoiceLinesIModule,
                InvNumOrderNum = c.InvNumOrderNum,
                InvNumTaxInclusive = c.InvNumTaxInclusive,
                InvNumDescription = c.InvNumDescription,
                BTBLInvoiceLinesIStockCodeId = c.BTBLInvoiceLinesIStockCodeId,
                ProjectKeyAccount = c.ProjectKeyAccount,
            }).ToList();


            //We might need to put column headers in an array of strings...
            List<string> colNames = new List<string>();
            colNames.Add("INVNUM.DOCTYPE");                         //A
            colNames.Add("INVNUM.INVDATE");                         //B
            colNames.Add("INVNUM.ACCOUNTID");                       //C
            colNames.Add("_BTBLINVOICELINES.ILEDGERACCOUNTID");     //D
            colNames.Add("_BTBLINVOICELINES.CDESCRIPTION");         //E
            colNames.Add("_BTBLINVOICELINES.FUNITPRICEEXCL");       //F
            colNames.Add("_BTBLINVOICELINES.ITAXTYPEID");           //G
            colNames.Add("INVNUM.EXTORDERNUM");                     //H
            colNames.Add("_BTBLINVOICELINES.FQUANTITY");            //I
            colNames.Add("_BTBLINVOICELINES.IMODULE");              //J
            colNames.Add("INVNUM.ORDERNUM");                        //K
            colNames.Add("INVNUM.TAXINCLUSIVE");                    //L
            colNames.Add("INVNUM.DESCRIPTION");                     //M
            colNames.Add("_BTBLINVOICELINES.ISTOCKCODEID");         //N
            colNames.Add("Project (Key Account)");                  //O

            return new ReportListVM<InvoiceDto>
            {
                Count = data.Count,
                Data = data,
                ColumnNames = colNames,
                ReportName = "Invoice : " + claimPeriod.ClaimYear.Name.ToString() + " " + claimPeriod.Name.ToString() + " - " + DateTime.Now,
            };
        }
    }
}
