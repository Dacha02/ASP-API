using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.Queries
{
    public interface IFindCarQuery : IQuery<int, ResponseSearchCarDto>
    {
    }
}
