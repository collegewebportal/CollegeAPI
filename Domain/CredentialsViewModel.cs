using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class CredentialsViewModel
    {
        [JsonProperty]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
