using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.DTO.SearchDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASPProjekatCarRental.Application.UseCases.DTO.SearchDto.PagedSearch;

namespace ASPProjekatCarRental.Application.UseCases.Queries
{
    public interface IGetCarsQuery : IQuery<BaseSearchWithIsRented, PagedResponse<ResponseSearchCarDto>>
    {
    }
}
