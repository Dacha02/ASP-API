using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.SearchDto
{
    public class UserSearchDto : PagedSearch
    {
        public string? Keyword { get; set; }
    }
}
