using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Services.Products.Models;
using Engage.Application.Services.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.OrderSkus.Queries
{
    public class GetOrderSkuProductsHistoryQuery : GetQuery, IRequest<List<ProductOptionDto>>
    {
    }

    public class GetOrderSkuProductsHistoryQueryHandler : BaseQueryHandler, IRequestHandler<GetOrderSkuProductsHistoryQuery, List<ProductOptionDto>>
    {
        private readonly IUserService _user;

        public GetOrderSkuProductsHistoryQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
        {
            _user = user;
        }

        public async Task<List<ProductOptionDto>> Handle(GetOrderSkuProductsHistoryQuery query, CancellationToken cancellationToken)
        {
            return await _context.OrderSkus.Where(e => e.CreatedBy.ToLower() == _user.UserName.ToLower())
                                           .OrderByDescending(e => e.OrderSkuId)
                                           .Join(_context.EngageVariantProducts,
                                                sku => sku.DCProduct.EngageVariantProductId,
                                                variantProduct => variantProduct.EngageVariantProductId,
                                                (sku, variantProduct) => new { sku, variantProduct })
                                           .Select(e => new ProductOptionDto
                                           {
                                               EngageMasterProductId = e.variantProduct.EngageMasterProductId,
                                               Id = e.variantProduct.EngageVariantProductId,
                                               DCProductId = e.sku.DCProduct.DCProductId,
                                               Name = e.sku.DCProduct.Code + " / " + e.variantProduct.Name + " / " + e.sku.DCProduct.Size + " " + e.sku.DCProduct.UnitType.Name
                                           })
                                           .Distinct()
                                           .Take(20)
                                           .ToListAsync(cancellationToken);
        }
    }
}

