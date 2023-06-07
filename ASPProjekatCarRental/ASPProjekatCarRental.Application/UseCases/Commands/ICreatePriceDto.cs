﻿using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.Commands
{
    public interface ICreatePriceDto : ICommand<ReceivePriceDto>
    {
    }
}
