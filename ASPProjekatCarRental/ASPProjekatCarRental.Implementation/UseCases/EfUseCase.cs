using ASPProjekatCarRental.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected EfUseCase(CarRentalContext context)
        {
            Context = context;
        }

        protected CarRentalContext Context { get; }
    }
}
