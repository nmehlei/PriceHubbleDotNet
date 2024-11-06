using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPriceHubble.Client.Valuation
{
    public class Address
    {
        public string? PostCode { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        /// <summary>
        /// For UK only
        /// </summary>
        public string? BuildingName { get; set; }
        /// <summary>
        /// For UK only
        /// </summary>
        public string? UnitIdentifier { get; set; }
    }
}