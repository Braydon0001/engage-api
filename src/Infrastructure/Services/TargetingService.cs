using Engage.Application.Services.Options.Models;
using Engage.Application.Targetings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace Engage.Infrastructure.Services
{
    public class TargetingService : ITargetingService
    {
        private readonly IAppDbContext _context;

        public TargetingService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> SerializeEmployeeCriteria(EmployeeTargetingCommand command, CancellationToken cancellationToken)
        {
            var criteria = new EmployeeTargetingCriteria
            {
                EngageDepartments = new List<OptionDto>(),
                EmployeeJobTitles = new List<OptionDto>()
            };

            if (command.EngageDepartments != null && command.EngageDepartments.Count > 0)
            {
                var query = _context.EngageDepartments.AsQueryable();
                criteria.EngageDepartments = await query.Where(e => command.EngageDepartments.Contains(e.Id))
                                                        .Select(e => new OptionDto(e.Id, e.Name))
                                                        .ToListAsync(cancellationToken);
            }

            if (command.JobTitles != null && command.JobTitles.Count > 0)
            {
                var query = _context.EmployeeJobTitles.AsQueryable();
                criteria.EmployeeJobTitles = await query.Where(e => command.JobTitles.Contains(e.EmployeeJobTitleId))
                                                        .Select(e => new OptionDto(e.EmployeeJobTitleId, e.Name))
                                                        .ToListAsync(cancellationToken);
            }

            var json = JsonConvert.SerializeObject(criteria);

            return json;
        }

        public async Task<string> SerializeStoreCriteria(StoreTargetingCommand command, CancellationToken cancellationToken)
        {
            var criteria = new StoreTargetingCriteria
            {
                EngageRegions = new List<OptionDto>(),
                StoreClusters = new List<OptionDto>(),
                StoreFormats = new List<OptionDto>(),
                StoreLSMs = new List<OptionDto>(),
                StoreTypes = new List<OptionDto>()
            };

            if (command.EngageRegions != null && command.EngageRegions.Count > 0)
            {
                var query = _context.EngageRegions.AsQueryable();
                criteria.EngageRegions = await query.Where(e => command.EngageRegions.Contains(e.Id))
                                                    .Select(e => new OptionDto(e.Id, e.Name))
                                                    .ToListAsync(cancellationToken);
            }

            if (command.StoreClusters != null && command.StoreClusters.Count > 0)
            {
                var query = _context.StoreClusters.AsQueryable();
                criteria.StoreClusters = await query.Where(e => command.StoreClusters.Contains(e.Id))
                                                    .Select(e => new OptionDto(e.Id, e.Name))
                                                    .ToListAsync(cancellationToken);
            }

            if (command.StoreFormats != null && command.StoreFormats.Count > 0)
            {
                var query = _context.StoreFormats.AsQueryable();
                criteria.StoreFormats = await query.Where(e => command.StoreFormats.Contains(e.Id))
                                                   .Select(e => new OptionDto(e.Id, e.Name))
                                                   .ToListAsync(cancellationToken);
            }

            if (command.StoreLSMs != null && command.StoreLSMs.Count > 0)
            {
                var query = _context.StoreLSMs.AsQueryable();
                criteria.StoreLSMs = await query.Where(e => command.StoreLSMs.Contains(e.Id))
                                                .Select(e => new OptionDto(e.Id, e.Name))
                                                .ToListAsync(cancellationToken);
            }

            if (command.StoreTypes != null && command.StoreTypes.Count > 0)
            {
                var query = _context.StoreTypes.AsQueryable();
                criteria.StoreTypes = await query.Where(e => command.StoreTypes.Contains(e.Id))
                                                 .Select(e => new OptionDto(e.Id, e.Name))
                                                 .ToListAsync(cancellationToken);
            }

            var json = JsonConvert.SerializeObject(criteria);

            return json;
        }

        public EmployeeTargetingCriteria DeserializeEmployeeCriteria(string criteria)
        {
            return JsonConvert.DeserializeObject<EmployeeTargetingCriteria>(criteria);
        }

        public StoreTargetingCriteria DeserializeStoreCriteria(string criteria)
        {
            return JsonConvert.DeserializeObject<StoreTargetingCriteria>(criteria);
        }

    }
}

