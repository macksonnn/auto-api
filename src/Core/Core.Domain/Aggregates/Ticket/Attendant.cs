using AutoMais.Ticket.Core.Domain.Aggregates.Attendant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket
{
    public class Attendant : Entity
    {
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string CardId { get; internal set; }

        public Attendant(AttendantAgg attendant)
        {
            Id = attendant.Id;
            Name = attendant.Name;
            CardId = attendant.CardId;
        }
    }
}
