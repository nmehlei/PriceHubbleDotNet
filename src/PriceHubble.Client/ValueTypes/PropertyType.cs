using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.ValueTypes
{
    public class PropertyType
    {
        public PropertyTypeCode Code { get; set; }
        public PropertyTypeSubcode? Subcode { get; set; }
    }
}