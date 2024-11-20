namespace Engage.Application.Services.Options.Queries;

public class GetUnassignedOptionListQuery : OptionsQuery, IRequest<List<OptionDto>>
{
    public int? Id { get; set; }

    public GetUnassignedOptionListQuery() { }
    public GetUnassignedOptionListQuery(string option, int? id = 0) : base(option)
    {
        Id = id;
    }
}

public class GetUnassignedOptionListQueryHandler : IRequestHandler<GetUnassignedOptionListQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public GetUnassignedOptionListQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(GetUnassignedOptionListQuery request, CancellationToken cancellationToken)
    {
        var optionName = request.Option.ToUpper();
        var id = request.Id ?? 0;

        var query = optionName switch
        {
            UnassignedOptionDesc.DCDEPTS => _context.DCDepartments
                    .FromSqlRaw("SELECT * FROM DCDepartments a WHERE a.DCDepartmentId NOT IN (SELECT b.DCDepartmentId FROM DCDepts b WHERE b.DistributionCenterId= {0})", id)
                    .Select(o => new { Id = o.DCDepartmentId, o.Name, o.Disabled }),
            UnassignedOptionDesc.EMPLOYEEDEPARTMENTS => _context.EngageDepartments
                    .FromSqlRaw("SELECT * FROM opt_EngageDepartments a WHERE a.Id NOT IN (SELECT b.EngageDepartmentId FROM EmployeeDepartments b WHERE b.EmployeeId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.EMPLOYEEREGIONS => _context.EngageRegions
                    .FromSqlRaw("SELECT * FROM opt_EngageRegions a WHERE a.Id NOT IN (SELECT b.EngageRegionId FROM EmployeeRegions b WHERE b.EmployeeId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.ENGAGEPRODUCTTAGS => _context.EngageTags
                   .FromSqlRaw("SELECT * FROM opt_EngageTags a WHERE a.Id NOT IN (SELECT b.EngageTagId FROM EngageProductTags b WHERE b.EngageMasterProductId = {0})", id)
                   .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.NOTIFICATIONREGIONS => _context.EngageRegions
                    .FromSqlRaw("SELECT * FROM opt_EngageRegions a WHERE a.Id NOT IN (SELECT b.EngageRegionId FROM NotificationRegions b WHERE b.NotificationId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.STORESTOREDEPARTMENTS => _context.StoreDepartments
                    .FromSqlRaw("SELECT * FROM opt_StoreDepartments a WHERE a.Id NOT IN (SELECT b.StoreDepartmentId FROM StoreStoreDepartments b WHERE b.StoreId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.STORESTORECONCEPTS => _context.StoreConcepts
                    .FromSqlRaw("SELECT * FROM opt_StoreConcepts a WHERE a.Id NOT IN (SELECT b.StoreConceptId FROM StoreStoreConcepts b WHERE b.StoreId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.SUPPLIERSUPPLIERTYPES => _context.SupplierTypes
                    .FromSqlRaw("SELECT * FROM opt_SupplierTypes a WHERE a.Id NOT IN (SELECT b.SupplierTypeId FROM SupplierSupplierTypes b WHERE b.SupplierId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.SUPPLIERENGAGEBRANDS => _context.EngageBrands
                    .FromSqlRaw("SELECT * FROM opt_EngageBrands a WHERE a.Id NOT IN (SELECT b.EngageBrandId FROM SupplierEngageBrands b WHERE b.SupplierId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.SURVEYQUESTIONFALSEREASONS => _context.QuestionFalseReasons
                    .FromSqlRaw("SELECT * FROM opt_QuestionFalseReasons a WHERE a.Id NOT IN (SELECT b.QuestionFalseReasonId FROM SurveyQuestionFalseReasons b WHERE b.SurveyQuestionId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.SURVEYENGAGEREGIONS => _context.EngageRegions
                    .FromSqlRaw("SELECT * FROM opt_EngageRegions a WHERE a.Id NOT IN (SELECT b.EngageRegionId FROM SurveyEngageRegions b WHERE b.SurveyId = {0})", id)
                    .Select(o => new { o.Id, o.Name, o.Disabled }),
            UnassignedOptionDesc.SURVEYSTORES => _context.Stores
                    .FromSqlRaw("SELECT a.StoreId, a.Name, a.Deleted, a.Disabled FROM Stores a WHERE a.StoreId NOT IN (SELECT b.StoreId FROM SurveyStores b WHERE b.SurveyId = {0})", id)
                    .Select(o => new { Id = o.StoreId, o.Name, o.Disabled }),
            _ => throw new UnknownOptionException(optionName),
        };

        return await query.Where(o => o.Disabled == false)
                          .Select(o => new OptionDto { Id = o.Id, Name = o.Name })
                          .OrderBy(o => o.Name)
                          .ToListAsync();
    }
}
