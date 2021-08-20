﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Entities
{
    public class Credentials
    {  
        [JsonProperty("username")]
        public string Username { get; set; }     
        [JsonProperty("password")]
        public string Password { get; set; }
        
    }
}
