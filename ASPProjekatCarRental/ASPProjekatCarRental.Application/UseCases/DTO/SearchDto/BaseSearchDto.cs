using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.SearchDto
{
    public class BaseSearchDto
    {
        public string? Keyword { get; set; }
    }

    public class PagedSearch
    {
        public int? PerPage { get; set; } = 15;
        public int? Page { get; set; } = 1;
    }

    public class BaseSearchWithIsRented : PagedSearch 
    {
        public string? Keyword { get; set; }
        public DateTime? StartOfRent { get; set; }
        public DateTime? EndOfRent { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class SearchForAuditLog : PagedSearch
    {
        public string? Keyword { get; set; }
        public string? Username { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }


}
