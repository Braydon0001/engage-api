using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Services.Options.Models;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.OrderEngageDepartments
{
    // Requests
    public class OrderEngageDepartmentsByEmployeeIdQuery : IRequest<ListResult<OptionDto>>
    {
        public int EmployeeId { get; set; }
        public int? OrderIdExclusion { get; set; }
    }

    public class OrderEngageDepartmentsByUserNameQuery : IRequest<ListResult<OptionDto>>
    {
        public string UserName { get; set; }
        public int? OrderIdExclusion { get; set; }
    }

    //TODO Move to Employees
    public class GetEmployeeEngageDepartmentsQuery : IRequest<List<OptionDto>>
    {
        
    }

    // Handlers
    public class OrderEngageDepartmentsByEmployeeIdQueryHandler : BaseQueryHandler, IRequestHandler<OrderEngageDepartmentsByEmployeeIdQuery, ListResult<OptionDto>>
    {
        public OrderEngageDepartmentsByEmployeeIdQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<OptionDto>> Handle(OrderEngageDepartmentsByEmployeeIdQuery query, CancellationToken cancellationToken)
        {
            //var sql = "SELECT * FROM opt_EngageDepartments a WHERE a.ID IN " +
            //          "(SELECT c.EngageDepartmentId FROM EmployeeDepartments c WHERE c.EmployeeId IN " +
            //          $"(SELECT d.EmployeeId FROM Employees d WHERE d.EmployeeId = {query.EmployeeId}))";

            var sql = @"select * 
                        from opt_engagedepartments engDept
                            left join EmployeeDepartments empDept
                                on engDept.Id = empDept.EngageDepartmentId
                        where engDept.Disabled = 0
                        and engDept.Deleted = 0
                        and empDept.EmployeeId = " + query.EmployeeId.ToString();

            if (query.OrderIdExclusion.HasValue)
            {
                sql += $" AND engDept.Id NOT IN(SELECT b.EngageDepartmentId FROM OrderEngageDepartments b WHERE b.OrderId = { query.OrderIdExclusion})";
            }

            return await OrderEngageDepartmentsUtils.GetOrderEngageDepartments(_context, sql, cancellationToken);
        }
    }

    public class OrderEngageDepartmentsByUserNameQueryHandler : BaseQueryHandler, IRequestHandler<OrderEngageDepartmentsByUserNameQuery, ListResult<OptionDto>>
    {
        public OrderEngageDepartmentsByUserNameQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<OptionDto>> Handle(OrderEngageDepartmentsByUserNameQuery query, CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM opt_EngageDepartments a WHERE a.ID IN " +
                      "(SELECT c.EngageDepartmentId FROM EmployeeDepartments c WHERE c.EmployeeId IN " +
                      $"(SELECT d.EmployeeId FROM Employees d WHERE d.EmailAddress1 LIKE '%{query.UserName}%'))";
            if (query.OrderIdExclusion.HasValue)
            {
                sql += $" AND a.Id NOT IN(SELECT b.EngageDepartmentId FROM OrderEngageDepartments b WHERE b.OrderId = { query.OrderIdExclusion})";
            }

            return await OrderEngageDepartmentsUtils.GetOrderEngageDepartments(_context, sql, cancellationToken);
        }
    }

    //TODO Move to Employees
    public class GetEmployeeEngageDepartmentsQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeEngageDepartmentsQuery, List<OptionDto>>
    {
        private readonly IUserService _user;

        public GetEmployeeEngageDepartmentsQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
        {
            _user = user;
        }

        public async Task<List<OptionDto>> Handle(GetEmployeeEngageDepartmentsQuery query, CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM opt_EngageDepartments a WHERE a.ID IN " +
                      "(SELECT c.EngageDepartmentId FROM EmployeeDepartments c WHERE c.EmployeeId IN " +
                      $"(SELECT d.EmployeeId FROM Employees d WHERE d.EmailAddress1 LIKE '%{_user.UserName}%'))";
            
            return await OrderEngageDepartmentsUtils.GetOrderEngageDepartmentsList(_context, sql, cancellationToken);
        }
    }

}
