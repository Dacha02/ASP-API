using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.DTO.SearchDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Queries
{
    public class EfGetAuditLogQuery : EfUseCase, IGetAuditLogs
    {
        public EfGetAuditLogQuery(CarRentalContext context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => "Get Audit Log Data Query";

        public string Description => "Get Audit Log Data from the table";

        public PagedResponse<ResponseSearchAuditLogDto> Execute(SearchForAuditLog request)
        {
            var auditLog = Context.AuditLogs.Include(x => x.User).AsQueryable();

            if(request.Keyword != null)
            {
                auditLog = auditLog.Where(x => x.UseCaseName.Contains(request.Keyword) ||
                                          x.User.UserName.Contains(request.Keyword));
            }

            if(request.DateFrom != null)
            {
                auditLog = auditLog.Where(x => x.TimeOfExecution >= request.DateFrom);
            }

            if (request.DateTo != null)
            {
                auditLog = auditLog.Where(x => x.TimeOfExecution <= request.DateTo);
            }

            if (request.PerPage == null || request.PerPage < 1)
            {
                request.PerPage = 15;
            }

            if (request.Page == null || request.Page < 1)
            {
                request.PerPage = 1;
            }

            var toSkip = (request.Page.Value - 1) * request.PerPage.Value;

            var result = new PagedResponse<ResponseSearchAuditLogDto>();

            result.TotalCount = auditLog.Count();

            result.Data = auditLog.Skip(toSkip).Take(request.PerPage.Value).Select(x => new ResponseSearchAuditLogDto
            {
                UserName = x.User.UserName,
                UseCaseName = x.UseCaseName,
                TimeOfExecuction = x.TimeOfExecution,
                Data = x.Data,
                IsAuthorized = x.IsAuthorized
            }).ToList();

            result.CurrentPage = request.Page.Value;
            result.ItemsPerPage = request.PerPage.Value;

            return result;
        }
    }
}
