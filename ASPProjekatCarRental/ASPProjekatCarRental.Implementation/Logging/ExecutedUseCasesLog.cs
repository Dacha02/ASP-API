using ASPProjekatCarRental.Application.UseCases;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Logging
{
    public class ExecutedUseCasesLog : IUseCaseLogger
    {
        private readonly CarRentalContext _context;

        public ExecutedUseCasesLog(CarRentalContext context)
        {
            _context = context;
        }

        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search)
        {
            throw new NotImplementedException();
        }

        public void Log(UseCaseLog log)
        {
            var auditLog = new AuditLog
            {
                IsAuthorized = log.IsAuthorized,
                UserId = log.UserId,
                UseCaseName = log.UseCaseName,
                Data = log.Data,
                TimeOfExecution = log.ExecutionDateTime
            };

            _context.AuditLogs.Add(auditLog);
            _context.SaveChanges();
        }
    }
}
