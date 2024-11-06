using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.Options
{
    public class PriceHubbleOptions
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public Uri BaseAddress { get; set; }
    }
}