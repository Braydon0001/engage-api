using Engage.Application.Services.ClaimEmailHistories.Queries;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.ClaimEmailHistories
{
    public class EmailHistoryPagination : PaginationBase<EmailHistory, PaginatedClaimEmailHistoryQuery>
    {
        private const string EMAIL_TEMPLATE_NAME = "EMAILTEMPLATENAME";
        private const string TO_EMAIL = "TOEMAIL";
        private const string ID = "ID";
        private const string SUBJECT = "SUBJECT";
        private const string USERNAME = "USERNAME";

        public EmailHistoryPagination(PaginatedClaimEmailHistoryQuery query) : base(query)
        {
        }

        protected override void Filter()
        {
        }

        protected override void AddFilterPredicates2(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }

        protected override void AddFilterPredicates()
        {
            foreach (var filter in _filters)
            {
                switch (filter.Key)
                {
                    case ID:
                        _predicates.Add(CreateFilter<EmailHistory>(filter, nameof(EmailHistory.EmailHistoryId)));
                        break;
                    case TO_EMAIL:
                        _predicates.Add(CreateFilter<EmailHistory>(filter, nameof(EmailHistory.ToEmail)));
                        break;
                    case EMAIL_TEMPLATE_NAME:
                        _predicates.Add(CreateFilter<EmailHistory>(filter, nameof(EmailHistory.EmailTemplate.Name)));
                        break;
                    case SUBJECT:
                        _predicates.Add(CreateFilter<EmailHistory>(filter, nameof(EmailHistory.Subject)));
                        break;
                    case USERNAME:
                        _predicates.Add(CreateFilter<EmailHistory>(filter, nameof(EmailHistory.CreatedBy)));
                        break;
                }
            }
        }

        protected override void Order(string orderBy, string orderDirection)
        {
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                var sortModel = JsonConvert.DeserializeObject<List<SortModel>>(orderBy);

                var isAsc = sortModel[0].Sort.ToUpper().Equals("ASC");
                var colId = sortModel[0].ColId.ToUpper();

                switch (colId)
                {
                    case ID:
                        { _queryable = isAsc ? _queryable.OrderBy(e => e.EmailHistoryId) : _queryable.OrderByDescending(e => e.EmailHistoryId); break; }
                }
            }
            else
            {
                _queryable = _queryable.OrderByDescending(e => e.EmailHistoryId);
            }
        }

        protected override int GetRowCount()
        {
            if (_filters.Count == 0)
            {
                try
                {
                    return _countQueryable.Select(e => e.EmailHistoryId).Max() - _countQueryable.Select(e => e.EmailHistoryId).Min() + 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return _countQueryable.Count();
        }
    }

    public static class EmailHistoryPaginationExtensions
    {
        public static (IQueryable<EmailHistory> queryable, PaginationResult pagedResult) Paginate(this IQueryable<EmailHistory> queryable, PaginatedClaimEmailHistoryQuery query, IMultiTenantContextAccessor multiTenantContextAccessor)
        {
            var pagination = new EmailHistoryPagination(query);
            return pagination.Paginate(queryable, multiTenantContextAccessor);
        }
    }
}
