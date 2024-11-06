using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.Auth
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
    }
}