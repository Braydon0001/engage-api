using Engage.Application.Interfaces;
using Engage.Application.Services.Options.Models;
using Engage.Application.Services.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.OrderEngageDepartments
{

    internal static class OrderEngageDepartmentsUtils
    {
        internal static async Task<ListResult<OptionDto>> GetOrderEngageDepartments(IAppDbContext context, string sql, CancellationToken cancellationToken)
        {
            var entities = await context.EngageDepartments
                .FromSqlRaw(sql)
                .Select(o => new { o.Id, o.Name })
                .Select(o => new OptionDto { Id = o.Id, Name = o.Name })
                .OrderBy(o => o.Name)
                .ToListAsync(cancellationToken);

            return new ListResult<OptionDto>
            {
                Data = entities,
                Count = entities.Count
            };
        }
        
        internal static async Task<List<OptionDto>> GetOrderEngageDepartmentsList(IAppDbContext context, string sql, CancellationToken cancellationToken)
        {
            return  await context.EngageDepartments
                .FromSqlRaw(sql)
                .Select(o => new { o.Id, o.Name })
                .Select(o => new OptionDto { Id = o.Id, Name = o.Name })
                .OrderBy(o => o.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
