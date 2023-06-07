﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class Specification : Entity
    {
        public string SpecificationName { get; set; }

        public virtual ICollection<SpecificationSpecificationValue> SpecificationValues { get; set; } = new List<SpecificationSpecificationValue>();
    }
}
