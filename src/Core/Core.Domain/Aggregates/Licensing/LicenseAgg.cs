﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Licensing
{
    public class LicenseAgg : AggRoot
    {
        public string LicenseKey { get; private set; }
        public string ProductName { get; private set; }
        public DateTime ActivationDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public DateTime DeactivationDate { get; private set; }


    }
}
