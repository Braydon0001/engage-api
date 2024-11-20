namespace Engage.Application.Services.Users.Queries;

public class ProjectSupplierUserOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class ProjectSupplierUserOptionsQueryHandler : IRequestHandler<ProjectSupplierUserOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ProjectSupplierUserOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ProjectSupplierUserOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Users.AsQueryable();

        var supplierIds = new List<int>();
        var storeIds = new List<int>();

        if (request.ProjectId != 0)
        {
            //var project = await _context.ProjectSuppliers.FirstOrDefaultAsync(v => v.ProjectId == request.ProjectId);

            var projectTagSupplierIds = await _context.ProjectProjectTagSuppliers.Where(e => e.ProjectId == request.ProjectId)
                                                                           .Select(e => e.SupplierId)
                                                                           .ToListAsync(cancellationToken);

            var projectStore = await _context.ProjectStores.FirstOrDefaultAsync(v => v.ProjectId == request.ProjectId);

            var projectTagStoreIds = await _context.ProjectProjectTagStores.Where(e => e.ProjectId == request.ProjectId)
                                                                           .Select(e => e.StoreId)
                                                                           .ToListAsync(cancellationToken);


            //if (project != null)
            //{
            //    supplierIds.Add(project.SupplierId);
            //}

            if (projectTagSupplierIds.Count > 0)
            {
                supplierIds.AddRange(projectTagSupplierIds);
            }

            if (projectStore != null)
            {
                storeIds.Add(projectStore.StoreId);
            }

            if (projectTagStoreIds.Count > 0)
            {
                storeIds.AddRange(projectTagStoreIds);
            }

            var userIds = new List<int>();

            var suppliers = supplierIds.Distinct().ToList();
            var stores = storeIds.Distinct().ToList();

            if (stores.Count > 0 && suppliers.Count > 0)
            {
                userIds = await _context.UserStores.Where(e => stores.Contains(e.StoreId) && suppliers.Contains(e.User.SupplierId))
                                                   .Select(e => e.UserId)
                                                   .ToListAsync(cancellationToken);
            }

            if (userIds.Count > 0)
            {
                queryable = queryable.Where(e => userIds.Contains(e.UserId));
            }
            else
            {
                return new List<OptionDto>();
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                queryable = queryable.Where(e => EF.Functions.Like(e.FirstName, $"%{request.Search}%")
                                                    || EF.Functions.Like(e.LastName, $"%{request.Search}%")
                                                    || EF.Functions.Like(e.Email, $"%{request.Search}%")
                                                  );
            }

            return await queryable.Where(e => e.Disabled == false)
                                  .Select(e => new OptionDto { Id = e.UserId, Name = e.FirstName + " " + e.LastName + " - " + e.Email })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
        }

        return new List<OptionDto>();
    }
}