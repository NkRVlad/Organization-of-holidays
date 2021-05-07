using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kudlay.Model
{
    public class CustomSettings
    {
        public JWTSettings JWTSettings { get; set; }
    }

    public class JWTSettings
    {
        public string Host { get; set; }
        public string SecretKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateAudience { get; set; }
    }
}
