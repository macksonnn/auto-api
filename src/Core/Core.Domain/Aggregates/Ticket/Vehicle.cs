
using AutoMais.Ticket.Core.Domain.Aggregates.Vehicle;

namespace AutoMais.Ticket.Core.Domain.Aggregates.Ticket
{
    public class Vehicle : Entity
    {
        public string Plate { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public string Situation { get; private set; }
        public string Image { get; private set; }
        public VehicleTypeEnum Type { get; private set; }


        public static Vehicle Create(VehicleAgg vehicle)
        {
            return new Vehicle()
            {
                Plate = vehicle.Plate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Color = vehicle.Color,
                Situation = vehicle.Situation,
                Image = vehicle.Image,
                Type = VehicleTypeEnum.Truck //TODO: Revisar como mapenar os tipos para o enum
            };
        }
    }
}
