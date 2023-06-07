using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.Exceptions
{
    public class FobiddenUseCaseExecutionException : Exception
    {
        public FobiddenUseCaseExecutionException(string useCase, string user)
            : base($"User {user} has tried to execute {useCase} without being authorized to do so.")
        {
        }
    }
}
