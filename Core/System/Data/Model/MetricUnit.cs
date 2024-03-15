﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class MetricUnit
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public MetricUnitCategory Category { get; set; }
    }
}
