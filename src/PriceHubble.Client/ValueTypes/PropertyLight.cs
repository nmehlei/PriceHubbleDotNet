using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.ValueTypes
{
    public class PropertyLight
    {
        public Location Location { get; set; }
        public PropertyType PropertyType { get; set; }
        public int? BuildingYear { get; set; }
        public decimal? LivingArea { get; set; }
        public decimal? LandArea { get; set; }
        public decimal? NumberOfRooms { get; set; }
        public bool? HasOpenKitchen { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public int? NumberOfBathrooms { get; set; }
        public Condition? Condition { get; set; }
    }
}