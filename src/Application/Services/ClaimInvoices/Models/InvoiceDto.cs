namespace Engage.Application.Services.ClaimInvoices.Models;

public class InvoiceDto : IMapFrom<ClaimSku>//IMapFrom<Claim>
{
    //Might have to hardcode columns for the file...
    //[JsonProperty("INVNUM.DOCTYPE")]
    public string InvNumDocType { get; set; } //4    -A

    //[JsonProperty("INVNUM.INVDATE")]
    public string InvNumInvDate { get; set; } //25/10/2021    -B

    //[JsonProperty("INVNUM.ACCOUNTID")]
    public string InvNumAccountId { get; set; } //MOAJHB      -C

    //[JsonProperty("_BTBLINVOICELINES.ILEDGERACCOUNTID")]
    public string BTBLInvoiceLinesILedgerAccountId { get; set; } //2400>010     -D

    //[JsonProperty("_BTBLINVOICELINES.CDESCRIPTION")]
    public string BTBLInvoiceLinesCDescription { get; set; } //ABAQULUSI SUPERSPARFree Stock115224     -E

    //[JsonProperty("_BTBLINVOICELINES.FUNITPRICEEXCL")]
    public decimal BTBLInvoiceLinesFUnitPriceExcl { get; set; } //445,50     -F

    //[JsonProperty("_BTBLINVOICELINES.ITAXTYPEID")]
    public string BTBLInvoiceLinesITaxTypeId { get; set; } //15     -G

    //[JsonProperty("INVNUM.EXTORDERNUM")]
    public string InvNumExtOrderNum { get; set; } //ENCORE CLAIMS OCT21     -H

    //[JsonProperty("_BTBLINVOICELINES.FQUANTITY")]
    public string BTBLInvoiceLinesFQuantity { get; set; } //1     -I

    //[JsonProperty("_BTBLINVOICELINES.IMODULE")]
    public string BTBLInvoiceLinesIModule { get; set; } //1     -J

    //[JsonProperty("INVNUM.ORDERNUM")]
    public string InvNumOrderNum { get; set; } //     -K

    //[JsonProperty("INVNUM.TAXINCLUSIVE")]
    public string InvNumTaxInclusive { get; set; } //     -L

    //[JsonProperty("INVNUM.DESCRIPTION")]
    public string InvNumDescription { get; set; } //     -M

    //[JsonProperty("_BTBLINVOICELINES.ISTOCKCODEID")]
    public string BTBLInvoiceLinesIStockCodeId { get; set; } //0     -N

    //[JsonProperty("Project (Key Account)")]
    public string ProjectKeyAccount { get; set; } //Angelique Botes     -O


    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<Claim, InvoiceDto>()
    //        .ForMember(d => d.InvNumDocType, opt => opt.MapFrom(s => 4)) //Fixed every month
    //        .ForMember(d => d.InvNumAccountId, opt => opt.MapFrom(s => "MOAJHB")) //Fixed every month
    //        .ForMember(d => d.BTBLInvoiceLinesILedgerAccountId, opt => opt.MapFrom(s => "2400>010")) //Fixed every month
    //        .ForMember(d => d.BTBLInvoiceLinesCDescription, opt => opt.MapFrom(s => s.Store.Name + s.ClaimType.Name + s.ClaimNumber)) //CONCATENATE STORE NAME/CLAIM TYPE/CLAIM NUMBER FROM MAPPER REPORT
    //        .ForMember(d => d.BTBLInvoiceLinesFUnitPriceExcl, opt => opt.MapFrom(s => s.ClaimSkus.Select(t => t.Amount).DefaultIfEmpty().Sum())) //CLAIM TOTAL / 1.15 TO GET EXC AMOUNT
    //        .ForMember(d => d.BTBLInvoiceLinesITaxTypeId, opt => opt.MapFrom(s => s.ClaimType.IsVatInclusive ? 3 : 15)) //15 IF VATABLE AND 3 IF NON VATABLE
    //        .ForMember(d => d.BTBLInvoiceLinesFQuantity, opt => opt.MapFrom(s => 1)) //Fixed every month
    //        .ForMember(d => d.BTBLInvoiceLinesIModule, opt => opt.MapFrom(s => 1)) //Fixed every month
    //        .ForMember(d => d.InvNumOrderNum, opt => opt.MapFrom(s => "")) //Fixed every month
    //        .ForMember(d => d.InvNumTaxInclusive, opt => opt.MapFrom(s => "")) //Fixed every month
    //        .ForMember(d => d.InvNumDescription, opt => opt.MapFrom(s => "")) //Fixed every month
    //        .ForMember(d => d.BTBLInvoiceLinesIStockCodeId, opt => opt.MapFrom(s => 0)) //Fixed every month
    //        .ForMember(d => d.ProjectKeyAccount, opt => opt.MapFrom(s => s.ClaimAccountManager.FullName)) //Whomever the KAM is
    //        .ForMember(d => d.InvNumExtOrderNum, opt => opt.MapFrom(s => s.ClaimAccountManager.FirstName));
    //}

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimSku, InvoiceDto>()
            .ForMember(d => d.InvNumDocType, opt => opt.MapFrom(s => 4)) //Fixed every month
            .ForMember(d => d.InvNumAccountId, opt => opt.MapFrom(s => "MOAJHB")) //Fixed every month
            .ForMember(d => d.BTBLInvoiceLinesILedgerAccountId, opt => opt.MapFrom(s => "2400>010")) //Fixed every month
            .ForMember(d => d.BTBLInvoiceLinesCDescription, opt => opt.MapFrom(s => s.Claim.Store.Name + s.Claim.ClaimType.Name + s.Claim.ClaimNumber)) //CONCATENATE STORE NAME/CLAIM TYPE/CLAIM NUMBER FROM MAPPER REPORT
            .ForMember(d => d.BTBLInvoiceLinesFUnitPriceExcl, opt => opt.MapFrom(s => s.Amount)) //CLAIM TOTAL / 1.15 TO GET EXC AMOUNT
            .ForMember(d => d.BTBLInvoiceLinesITaxTypeId, opt => opt.MapFrom(s => ((s.Claim.ClaimType.IsVatInclusive) || (s.DCProduct.EngageVariantProduct.EngageMasterProduct.VatId == 2)) ? 3 : 15)) //15 IF VATABLE AND 3 IF NON VATABLE
            .ForMember(d => d.BTBLInvoiceLinesFQuantity, opt => opt.MapFrom(s => 1)) //Fixed every month
            .ForMember(d => d.BTBLInvoiceLinesIModule, opt => opt.MapFrom(s => 1)) //Fixed every month
            .ForMember(d => d.InvNumOrderNum, opt => opt.MapFrom(s => "")) //Fixed every month
            .ForMember(d => d.InvNumTaxInclusive, opt => opt.MapFrom(s => "")) //Fixed every month
            .ForMember(d => d.InvNumDescription, opt => opt.MapFrom(s => "")) //Fixed every month
            .ForMember(d => d.BTBLInvoiceLinesIStockCodeId, opt => opt.MapFrom(s => 0)) //Fixed every month
            .ForMember(d => d.ProjectKeyAccount, opt => opt.MapFrom(s => s.Claim.ClaimAccountManager.FullName)) //Whomever the KAM is
            .ForMember(d => d.InvNumExtOrderNum, opt => opt.MapFrom(s => s.Claim.ClaimAccountManager.FirstName));
    }
}
