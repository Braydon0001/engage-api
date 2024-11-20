using Engage.Application.Services.StoreConceptAttributes.Models;
using Engage.Application.Services.StoreConceptAttributeValues.Models;

namespace Engage.Application.Services.StoreConceptAttributeValues.Queries
{
    public class StoreConceptAttributeValueQuery : IRequest<List<StoreConceptAttributeValueDto>>
    {
        public int StoreId { get; set; }
        public int StoreConceptId { get; set; }
    }

    public class StoreConceptAttributeValueQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptAttributeValueQuery, List<StoreConceptAttributeValueDto>>
    {
        public StoreConceptAttributeValueQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreConceptAttributeValueDto>> Handle(StoreConceptAttributeValueQuery request, CancellationToken cancellationToken)
        {
            var queryableAttributes = _context.StoreConceptAttributes.AsQueryable();

            queryableAttributes = queryableAttributes.Where(e => e.StoreConceptId == request.StoreConceptId);

            var queryableValues = _context.StoreConceptAttributeValues.AsQueryable();

            queryableValues = queryableValues.Where(e => e.StoreId == request.StoreId && e.StoreConceptAttribute.StoreConceptId == request.StoreConceptId);

            var values = await queryableValues.ProjectTo<StoreConceptAttributeValueDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            var attributes = await queryableAttributes.ProjectTo<StoreConceptAttributeDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            var attributeIds = values.Select(e => e.StoreConceptAttributeId).ToList();

            var diff = attributes.Where(e => !(attributeIds.Contains(e.Id)));

            foreach (var entry in diff)
            {
                var newValueDto = new StoreConceptAttributeValueDto
                {
                    StoreConceptAttributeId = entry.Id,
                    StoreId = (int)request.StoreId,
                    Value = null,
                    StoreConceptAttributeName = entry.Name,
                    StoreConceptAttributeTypeId = entry.StoreConceptAttributeTypeId,
                    StoreConceptAttributeTypeName = entry.StoreConceptAttributeTypeName,
                };

                values.Add(newValueDto);
            }

            return new List<StoreConceptAttributeValueDto>(values);
        }
    }
}