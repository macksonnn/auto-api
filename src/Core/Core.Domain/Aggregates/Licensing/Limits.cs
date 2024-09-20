using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Aggregates.Licensing
{
    public class Limits : Entity
    {
        public int MaxUsers { get; private set; }
        public int MaxDevices { get; private set; }
        public int MaxLiters { get; private set; }
        public int MaxFuelingCount { get; private set; }
    }
}
