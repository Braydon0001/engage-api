using Engage.Application.Services.StoreConceptAttributeValues.Models;
using Attribute = Engage.Application.Services.StoreConceptAttributeValues.Models.Attribute;

namespace Engage.Application.Services.StoreConceptAttributeValues.Queries
{
    public class StoreConceptAttributeValueGroupQuery : IRequest<List<StoreConceptAttributeValueGroup>>
    {
        public int StoreId { get; set; }
        public int StoreConceptId { get; set; }
    }

    public class StoreConceptAttributeValueGroupQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptAttributeValueGroupQuery, List<StoreConceptAttributeValueGroup>>
    {
        public StoreConceptAttributeValueGroupQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreConceptAttributeValueGroup>> Handle(StoreConceptAttributeValueGroupQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreConceptAttributeValues.AsQueryable();

            queryable = queryable.Where(e => e.StoreId == request.StoreId && e.StoreConceptAttribute.StoreConceptId == request.StoreConceptId);

            var entities = await queryable.ProjectTo<StoreConceptAttributeValueDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            var storeName = _context.Stores.Where(e => e.StoreId == request.StoreId).FirstOrDefault().Name;

            var conceptName = _context.StoreConcepts.Where(e => e.Id == request.StoreConceptId).FirstOrDefault().Name;

            var StoreConceptAttributeValueData = new StoreConceptAttributeValueGroup {
                StoreId = request.StoreId,
                StoreName = storeName,
                ConceptId = request.StoreConceptId,
                ConceptName = conceptName,
                Attributes = new List<Attribute>(),
            };

            foreach (var entity in entities)
            {
                var storeConceptAttributeEntity = _context.StoreConceptAttributes.Where(e => e.StoreConceptAttributeId == entity.StoreConceptAttributeId).FirstOrDefault();
                var attributeName = storeConceptAttributeEntity.Name;
                var attributeType = storeConceptAttributeEntity.StoreConceptAttributeTypeId;
                var attr = new Attribute
                {
                    StoreConceptAttributeValueId = entity.Id,
                    AttributeId = entity.StoreConceptAttributeId,
                    AttributeName = attributeName,
                    AttributeType = attributeType,
                    Value = entity.Value,
                };
                StoreConceptAttributeValueData.Attributes.Add(attr);
            }

            var StoreConceptAttributeValueList = new List<StoreConceptAttributeValueGroup>()
            {
                StoreConceptAttributeValueData
            };

            return StoreConceptAttributeValueList;
        }
    }
}