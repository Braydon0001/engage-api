using Engage.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.Suppliers.Queries
{
    public class ValidateSupplierCodeQuery: IRequest<bool>
    {
        public string Code { get; set; }
    }

    public class ValidateSupplierCodeQueryHandler : IRequestHandler<ValidateSupplierCodeQuery, bool>
    {
        private readonly IAppDbContext _context;

        public ValidateSupplierCodeQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ValidateSupplierCodeQuery request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers.Where(e => e.Code == request.Code)
                                           .AnyAsync(cancellationToken);
        }   
    }
}
