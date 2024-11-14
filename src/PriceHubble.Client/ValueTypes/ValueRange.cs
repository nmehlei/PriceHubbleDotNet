using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.ValueTypes
{
    public class ValueRange
    {
        public int Lower { get; set; }
        public int Upper { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Lower, Upper);
        }
    }
}