﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryFlooring.Models.Interfaces
{
    public interface ITaxRepository
    {
        List<Tax> GetTaxes();
    }
}
