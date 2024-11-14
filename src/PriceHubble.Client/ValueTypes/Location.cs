using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.ValueTypes
{
    public class Location
    {
        public Coordinates? Coordinates { get; set; }
        public Address? Address { get; set; }
        public int? Uprn { get; set; }
    }
}