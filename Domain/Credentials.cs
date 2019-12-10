using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    class Credentials
    {
        [JsonProperty]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
