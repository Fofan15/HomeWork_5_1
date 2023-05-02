using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Dtos.Responses
{
    public class RegisterResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
