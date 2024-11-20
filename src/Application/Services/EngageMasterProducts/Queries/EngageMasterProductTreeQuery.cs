using Engage.Application.Services.DCProducts.Models;
using Engage.Application.Services.EngageMasterProducts.Models;
using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public class EngageMasterProductTreeQuery : PaginatedQuery, IRequest<ListResult<ProductTreeDto>>
{
    public string Search { get; set; }
}

public record EngageMasterProductTreeHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EngageMasterProductTreeQuery, ListResult<ProductTreeDto>>
{
    public async Task<ListResult<ProductTreeDto>> Handle(EngageMasterProductTreeQuery query, CancellationToken cancellationToken)
    {
        var search = query.Search;

        if (search.IsNullOrEmpty())
        {
            var queryable = Context.EngageMasterProducts.AsQueryable()
                                                        .AsNoTracking()
                                                        .Include(e => e.EngageVariantProducts);

            var masterProducts = await queryable.OrderBy(e => e.Name)
                                                .SkipQuery(query)
                                                .TakeQuery(query)
                                                .ProjectTo<EngageMasterProductListDto>(Mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

            return new(masterProducts.Select(e => new ProductTreeDto
            {
                Id = e.Id,
                Type = "Master",
                IsParent = true,
                Code = e.Code,
                Name = e.Name,
                EngageSubCategory = e.EngageSubCategoryName,
                Disabled = e.Disabled,
                Deleted = e.Deleted,
            }).OrderBy(e => e.Name).ToList());
        }

        // Level 0 - Master Product  
        if (query.GroupKeys.Count == 0)
        {
            var masterProducts = await Context.EngageMasterProducts.AsQueryable()
                                                                   .AsNoTracking()
                                                                   .Include(e => e.EngageVariantProducts)
                                                                   .Where(e => EF.Functions.Like(e.Code, $"%{search}%") || EF.Functions.Like(e.Name, $"%{search}%"))
                                                                   .OrderBy(e => e.Name)
                                                                   .SkipQuery(query)
                                                                   .TakeQuery(query)
                                                                   .ProjectTo<EngageMasterProductListDto>(Mapper.ConfigurationProvider)
                                                                   .ToListAsync(cancellationToken);

            var variantProducts = await Context.EngageVariantProducts.AsQueryable()
                                                                     .AsNoTracking()
                                                                     .Include(e => e.DCProducts)
                                                                     .Where(e => EF.Functions.Like(e.Code, $"%{search}%") || EF.Functions.Like(e.Name, $"%{search}%"))
                                                                     .OrderBy(e => e.Name)
                                                                     .SkipQuery(query)
                                                                     .TakeQuery(query)
                                                                     .ProjectTo<EngageVariantProductListDto>(Mapper.ConfigurationProvider)
                                                                     .ToListAsync(cancellationToken);

            var dcProducts = await Context.DCProducts.AsQueryable()
                                                     .AsNoTracking()
                                                     .Where(e => EF.Functions.Like(e.Code, $"%{search}%") || EF.Functions.Like(e.Name, $"%{search}%"))
                                                     .OrderBy(e => e.Name)
                                                     .SkipQuery(query)
                                                     .TakeQuery(query)
                                                     .ProjectTo<DCProductListDto>(Mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

            var masterProductIds = masterProducts.Select(e => e.Id);
            var variantProductIds = variantProducts.Select(e => e.Id);
            var dcProductIds = dcProducts.Select(e => e.Id);

            // Walk up the tree. Add missing variants. 
            foreach (var dc in dcProducts.Where(e => !variantProductIds.Contains(e.EngageVariantProductId)))
            {
                variantProducts.Add(new EngageVariantProductListDto
                {
                    EngageMasterProductId = dc.EngageMasterProductId,
                    EngageMasterProductCode = dc.EngageMasterProductCode,
                    EngageMasterProductName = dc.EngageMasterProductName,
                    EngageMasterProductSubCategoryName = dc.EngageMasterProductSubCategoryName,
                    EngageMasterProductDisabled = dc.EngageMasterProductDisabled,
                    EngageMasterProductDeleted = dc.EngageMasterProductDeleted,
                });
            }

            // Walk up the tree. Add missing masters. 
            foreach (var variant in variantProducts.Where(e => !masterProductIds.Contains(e.EngageMasterProductId)))
            {
                masterProducts.Add(new EngageMasterProductListDto
                {
                    Id = variant.EngageMasterProductId,
                    Code = variant.EngageMasterProductCode,
                    Name = variant.EngageMasterProductName,
                    EngageSubCategoryName = variant.EngageMasterProductSubCategoryName,
                    Disabled = variant.EngageMasterProductDisabled,
                    Deleted = variant.EngageMasterProductDeleted
                });
            }

            var products = masterProducts.Select(e => new ProductTreeDto
            {
                Id = e.Id,
                Type = "Master",
                IsParent = true,
                Code = e.Code,
                Name = e.Name,
                EngageSubCategory = e.EngageSubCategoryName,
                Disabled = e.Disabled,
                Deleted = e.Deleted,
            }).
            OrderBy(e => e.Name).ToList();

            return new(products);
        }

        // Level 1 - Variant Product  
        if (query.GroupKeys.Count == 1)
        {

            var masterProductId = int.Parse(query.GroupKeys[0]);
            var variantProducts = await Context.EngageVariantProducts.Where(e => e.EngageMasterProductId == masterProductId)
                                                                     .OrderBy(e => e.Name)
                                                                     .ProjectTo<EngageVariantProductListDto>(Mapper.ConfigurationProvider)
                                                                     .Select(e => new ProductTreeDto
                                                                     {
                                                                         Id = e.Id,
                                                                         Type = "Variant",
                                                                         IsParent = true,
                                                                         Code = e.Code,
                                                                         Name = e.Name,
                                                                         Disabled = e.Disabled,
                                                                         Deleted = e.Deleted,
                                                                     })
                                                                      .ToListAsync(cancellationToken);

            return new(variantProducts);
        }

        // Level 2 - DC Product  
        if (query.GroupKeys.Count == 2)
        {
            var variantProductId = int.Parse(query.GroupKeys[1]);
            var dcProductDtos = await Context.DCProducts.Where(e => e.EngageVariantProductId == variantProductId)
                                                        .OrderBy(e => e.Name)
                                                        .ProjectTo<DCProductListDto>(Mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);


            var dcProducts = dcProductDtos.Select(e => new ProductTreeDto
            {
                Id = e.Id,
                Type = $"DC - {e.DistributionCenter}",
                IsParent = false,
                Code = e.Code,
                Name = e.Name,
                ProductActiveStatus = e.ProductActiveStatusName,
                ProductStatus = e.ProductStatusName,
                ProductWarehouseStatus = e.ProductWarehouseStatusName,
                ProductSubWarehouse = e.ProductSubWarehouseName,
                Disabled = e.Disabled,
                Deleted = e.Deleted,
            }).ToList();

            return new(dcProducts);
        }

        throw new ArgumentException("Invalid GroupKeys");
    }
}