using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERADapi
{
    public static class Constants
    {
        // suppose to be ERAD or Customs 
        public const string Issuer = "https://localhost:44330/";
        // here suppose to put SSO Url and Customs Url
        public const string Audiance = "https://localhost:44330/";
        public const string Secret = "not_too_short_secret_otherwise_it_might_error";
    }
}
