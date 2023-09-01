using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
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
    public class EfGetProfitsQuery : EfUseCase, IGetProfitsQuery
    {
        public EfGetProfitsQuery(CarRentalContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Query for getting profits";

        public string Description => "Query for getting profits";

        public ProfitsDto Execute(DummyClass request)
        {
            var currentDate = DateTime.Today;
            var startDate = currentDate.AddYears(-1).AddMonths(1);

            var query = Context.Rentings
                .Where(renting => renting.StartDate >= startDate && renting.StartDate <= currentDate)
                .GroupBy(renting => new
                {
                    Year = renting.StartDate.Year,
                    Month = renting.StartDate.Month
                })
                .Select(grouped => new
                {
                    Year = grouped.Key.Year,
                    Month = grouped.Key.Month,
                    MonthlyProfit = grouped.Sum(r => r.SumCost), // Use Sum instead of Average
                    AnnualProfit = grouped.Sum(r => r.SumCost)
                });

            var yearlyProfit = query.Sum(profit => profit.AnnualProfit); // Sum of annual profits
            var monthlyProfit = query.Average(profit => profit.MonthlyProfit); // Average of monthly profits

            var byMonthProfits = new decimal[12];
            foreach (var profit in query)
            {
                byMonthProfits[profit.Month - 1] = (decimal)profit.MonthlyProfit;
            }

            var response = new ProfitsDto
            {
                Yearly = (double)yearlyProfit,
                Monthly = (double)monthlyProfit,
                ByMonth = byMonthProfits.ToList()
            };

            return response;
        }
    }
}
